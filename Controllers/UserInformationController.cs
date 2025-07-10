using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OISPublic.Helper;
using OISPublic.OISDataRoom;
using OISPublic.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OISPublic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInformationController : ControllerBase
    {
        private readonly OISDataRoomContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly NotificationService _notificationService;

        public UserInformationController(
            OISDataRoomContext context,
            IConfiguration configuration,
            JwtTokenService jwtTokenService,
              IHubContext<NotificationHub> hubContext,
              NotificationService notificationService)
        {
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        [HttpGet("UserInfo/{id}")]
        public async Task<IActionResult> GetUserDetailById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid request. 'Id' is required.");

            var masterUser = await _context.DataRoomMasterUsers
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (masterUser == null)
                return NotFound("User not found in DataRoomMasterUsers.");

            var userRoomLinks = await _context.DataRoomUsers
                .Where(x => x.UserId == id && x.IsDeleted ==  false)
                .ToListAsync();

            var dataRoomIds = userRoomLinks.Select(x => x.DataRoomId).Distinct().ToList();

     
            var allDataRooms = await _context.DataRooms
                .Where(dr => dataRoomIds.Contains(dr.Id) && !dr.IsDeleted)
                .ToListAsync();

            var currentDate = DateTime.UtcNow;

            var activeRooms = allDataRooms
                .Where(dr => dr.ExpirationDate == null || dr.ExpirationDate > currentDate)
                .ToList();

            var expiredRooms = allDataRooms
                .Where(dr => dr.ExpirationDate != null && dr.ExpirationDate <= currentDate)
                .ToList();

         
            var roomFiles = await _context.DataRoomFiles
                .Where(f => dataRoomIds.Contains(f.DataRoomId) && !f.IsDeleted)
                .ToListAsync();

            var documentIds = roomFiles
                .Where(f => f.DocumentId != null)
                .Select(f => f.DocumentId!)
                .Distinct()
                .ToList();

            var documents = await _context.Documents
       .Where(d => documentIds.Contains(d.Id) && !d.IsDeleted)
       .Select(d => new
       {
           d.Id,
           Name = d.Name ?? string.Empty,
           Description = d.Description ?? string.Empty,
           DocumentType = d.DocumentType ?? string.Empty,
           DocType = d.DocType ?? string.Empty,
           DocumentSize = d.DocumentSize ?? 0,  
           CreatedDate = d.CreatedDate
       })
       .ToListAsync();

            var companyNames = allDataRooms
     .Where(dr => !string.IsNullOrWhiteSpace(dr.CompanyName))
     .Select(dr => dr.CompanyName)
     .Distinct()
     .ToList();
            return Ok(new
            {
                User = new
                {
                    masterUser.Id,
                    masterUser.Name,
                    masterUser.Password,
                    masterUser.Email,
                    masterUser.IsActive,
                    masterUser.IsExternal,
                    masterUser.ClientId,
                    masterUser.CompanyId
                },
                CompanyNames = companyNames,
                TotalDocuments = documents.Count(),      
                TotalDataRooms = allDataRooms.Count(),   
                ActiveDataRooms = activeRooms.Select(r => new
                {
                    r.Id,
                    r.Name,
                    r.Description,
                    r.ExpirationDate,
                    r.DefaultPermission,
                    r.ClientId,
                    r.CompanyId
                }),
                ExpiredDataRooms = expiredRooms.Select(r => new
                {
                    r.Id,
                    r.Name,
                    r.Description,
                    r.ExpirationDate,
                    r.DefaultPermission,
                    r.ClientId,
                    r.CompanyId
                }),
                Documents = documents
            });

        }


        public class UpdatePasswordRequest
        {
            public Guid UserId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string OldPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }


        [HttpPut("UpdateUserDetails")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if (request.UserId == Guid.Empty ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.OldPassword) ||
                string.IsNullOrWhiteSpace(request.NewPassword) ||
                string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("UserId, Email, OldPassword, and NewPassword are required.");
            }

            var user = await _context.DataRoomMasterUsers
                .FirstOrDefaultAsync(x => x.Id == request.UserId && x.Email == request.Email && x.IsDeleted == false);

            if (user == null)
                return NotFound("User not found or email does not match.");

            if (user.Password != request.OldPassword)
                return BadRequest("Old password does not match our records.");

            if (user.Password == request.NewPassword)
                return BadRequest("New password must be different from the old password.");

            // Update name and password
            user.Name = request.Name;
            user.Password = request.NewPassword;

            _context.DataRoomMasterUsers.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password updated successfully." });
        }

        [HttpGet("UserNotification/{userId}")]
        public async Task<IActionResult> GetUserNotifications(Guid userId)
        {
            var notifications = await _context.DataRoomNotifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            // Notify the user's group
            await _hubContext.Clients.Group(userId.ToString()).SendAsync("NotificationUpdated", new
            {
                Count = notifications.Count,
                Timestamp = DateTime.UtcNow
            });

            return Ok(notifications);
        }

        //[HttpGet("UserNotification/{userId}")]
        //public async Task<IActionResult> GetUserNotifications(Guid userId)
        //{
        //    var notifications = await _notificationService.SendUserNotificationsAsync(userId);
        //    return Ok(notifications);
        //}


        public class MarkAsReadDto
        {
            public bool IsRead { get; set; }
        }

        [HttpPut("MarkAsRead/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId, [FromBody] MarkAsReadDto dto)
        {
            var notification = await _context.DataRoomNotifications.FindAsync(notificationId);
            if (notification == null) return NotFound();

            notification.IsRead = dto.IsRead;
            notification.ReadAt = dto.IsRead ? DateTime.UtcNow : null;

            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
