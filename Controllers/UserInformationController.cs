using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OISPublic.Helper;
using OISPublic.OISDataRoom;
using OISPublic.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OISPublic.Helper.AesEncryptionHelper;

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






            var result = new
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
                    masterUser.CompanyId,
                    masterUser.ProfilePicturePath
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
            };

            string json = JsonConvert.SerializeObject(result);
            EncryptedResult encrypted = AesEncryptionHelper.EncryptWithRandomKey(json);

            return Ok(encrypted);
        }


        public class UpdatePasswordRequest
        {
            public Guid UserId { get; set; }
            public string Email { get; set; } = "";
            public string Name { get; set; } = "";
            public string OldPassword { get; set; } = "";
            public string? NewPassword { get; set; }
            public string? ConfirmPassword { get; set; }
        }


        [HttpPost("UpdateUserDetails")]
        public async Task<IActionResult> UpdatePasswordWithProfile([FromForm] UpdatePasswordRequest request)
        {
            // Access profilePic directly from Request.Form.Files
            var profilePic = Request.Form.Files["profilePicture"];

            // Basic validation
            if (request.UserId == Guid.Empty ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.OldPassword) ||
                string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "UserId, Email, OldPassword, and Name are required."
                });
            }

            // Find the user
            var user = await _context.DataRoomMasterUsers
                .FirstOrDefaultAsync(x => x.Id == request.UserId && x.Email == request.Email && x.IsDeleted == false);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found or email does not match."
                });
            }

            // Validate old password
            if (user.Password != request.OldPassword)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Old password does not match our records."
                });
            }

            // If new password is provided, validate and update
            if (!string.IsNullOrWhiteSpace(request.NewPassword) || !string.IsNullOrWhiteSpace(request.ConfirmPassword))
            {
                if (string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Both new and confirm password are required to change password."
                    });
                }

                if (request.NewPassword != request.ConfirmPassword)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "New password and confirm password do not match."
                    });
                }

                if (request.NewPassword == request.OldPassword)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "New password must be different from the old password."
                    });
                }

                user.Password = request.NewPassword;
            }

          
            user.Name = request.Name;

            if (profilePic != null && profilePic.Length > 0)
            {
                var storagePath = await _context.FileStorageSettings
                    .Where(s => !s.IsDeleted)
                    .Select(s => s.Path)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(storagePath))
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Image storage path is not configured. Cannot save profile picture."
                    });
                }

                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }

                var fileExtension = Path.GetExtension(profilePic.FileName);
                var fileName = $"{request.UserId}{fileExtension}";
                var fullPath = Path.Combine(storagePath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await profilePic.CopyToAsync(stream);
                }

                user.ProfilePicturePath = fullPath;
            }

            _context.DataRoomMasterUsers.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "User details updated successfully."
            });
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

            //return Ok(notifications);

            string json = JsonConvert.SerializeObject(notifications);
            EncryptedResult encrypted = AesEncryptionHelper.EncryptWithRandomKey(json);

            return Ok(encrypted);
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
