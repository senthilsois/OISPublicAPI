using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;
using OISPublic.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OISPublic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActiveExpiredAuditRoomController : ControllerBase
    {
        private readonly OISDataRoomContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public ActiveExpiredAuditRoomController(
            OISDataRoomContext context,
            IConfiguration configuration,
            JwtTokenService jwtTokenService)
        {
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }
        [HttpGet("Active-Expired/{id}")]
        public async Task<IActionResult> ActiveExpired([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid request. 'Id' is required.");

            var masterUser = await _context.DataRoomMasterUsers
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (masterUser == null)
                return NotFound("User not found in DataRoomMasterUsers.");

            var relatedUserRooms = await _context.DataRoomUsers
                .Where(x => x.UserId == masterUser.Id && x.IsDeleted == false)
                .ToListAsync();

            if (!relatedUserRooms.Any())
                return NotFound("No related data rooms found for this user.");

            var roomIds = relatedUserRooms.Select(x => x.DataRoomId).Distinct().ToList();

            var relatedDataRooms = await _context.DataRooms
                .Where(r => roomIds.Contains(r.Id) && r.IsDeleted == false)
                .ToListAsync();

            var relatedFiles = await _context.DataRoomFiles
                .Where(f => roomIds.Contains(f.DataRoomId) && f.IsDeleted == false)
                .ToListAsync();

            var documentIds = relatedFiles
                .Where(f => f.DocumentId != null)
                .Select(f => f.DocumentId)
                .Distinct()
                .ToList();

            var relatedDocuments = await _context.Documents
                .Where(d => documentIds.Contains(d.Id) && d.IsDeleted == false)
                .ToListAsync();

            var currentDate = DateTime.UtcNow;

            var activeRooms = relatedDataRooms
                .Where(r => r.ExpirationDate == null || r.ExpirationDate > currentDate)
                  .OrderByDescending(r => r.CreatedAt)
                .Select(r => new
                {
                    Room = r,
                    Files = relatedFiles
                        .Where(f => f.DataRoomId == r.Id)
                        .Select(f => new
                        {
                            //File = f,
                            Document = relatedDocuments.FirstOrDefault(d => d.Id == f.DocumentId)
                        }).ToList()
                })
                .ToList();

            var expiredRooms = relatedDataRooms
                .Where(r => r.ExpirationDate != null && r.ExpirationDate <= currentDate)
                 .OrderByDescending(r => r.ExpirationDate)
                .Select(r => new
                {
                    Room = r,
                    Files = relatedFiles
                        .Where(f => f.DataRoomId == r.Id)
                        .Select(f => new
                        {
                            //File = f,
                            Document = relatedDocuments.FirstOrDefault(d => d.Id == f.DocumentId)
                        }).ToList()
                })
                .ToList();

            // Build permissions
            var dataRoomPermissions = relatedDataRooms
                .Select(r => new
                {
                    DataRoomId = r.Id,
                    Permission = r.DefaultPermission
                }).ToList();

            var userPermissions = relatedUserRooms
                .Select(u => new
                {
                    UserId = u.UserId,
                    Permission = u.AccessLevel
                }).ToList();

            var documentPermissions = relatedFiles
                .Select(f => new
                {
                    DocumentId = f.DocumentId,
                    Permission = f.Permission
                }).ToList();

            return Ok(new
            {
                User = masterUser,
                ActiveRooms = activeRooms,
                ExpiredRooms = expiredRooms,
                //UserRoomLinks = relatedUserRooms,
                Permissions = new
                {
                    DataRoomPermissions = dataRoomPermissions,
                    UserPermissions = userPermissions,
                    DocumentPermissions = documentPermissions
                }
            });
        }




        ////below is for dataroom details by id
        ///

        [HttpGet("DataRoomDetails/{userId}/{dataRoomId}")]
        public async Task<IActionResult> GetDataRoomDetails([FromRoute] Guid userId, [FromRoute] Guid dataRoomId)
        {
            if (userId == Guid.Empty || dataRoomId == Guid.Empty)
                return BadRequest("Both userId and dataRoomId are required.");

            var masterUser = await _context.DataRoomMasterUsers
                .FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == false);

            if (masterUser == null)
                return NotFound("User not found in DataRoomMasterUsers.");

            var userRoom = await _context.DataRoomUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.DataRoomId == dataRoomId && x.IsDeleted == false);

            if (userRoom == null)
                return Forbid("User does not have access to the specified Data Room.");

            var dataRoom = await _context.DataRooms
                .FirstOrDefaultAsync(r => r.Id == dataRoomId && r.IsDeleted == false);

            if (dataRoom == null)
                return NotFound("Data Room not found.");

            var files = await _context.DataRoomFiles
                .Where(f => f.DataRoomId == dataRoomId && f.IsDeleted == false)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            var documentIds = files
                .Where(f => f.DocumentId != null)
                .Select(f => f.DocumentId)
                .Distinct()
                .ToList();

            var documents = await _context.Documents
                .Where(d => documentIds.Contains(d.Id) && d.IsDeleted == false)
                .ToListAsync();

            var fileDetails = files.Select(f => new
            {
                DocumentId = f.DocumentId,
                DocumentPermission = f.Permission,
                Document = documents.FirstOrDefault(d => d.Id == f.DocumentId)
            }).ToList();


            var auditLogs = await _context.DataRoomAuditTrials
       .Where(a => a.DataRoomId == dataRoomId && a.IsDeleted == false &&a.CreatedBy == userId)
       .OrderByDescending(a => a.CreatedDate)
       .Select(a => new
       {
           a.Id,
           a.Operation,
           a.DataRoomName,
          
           a.ActionName,
           a.DocumentName,
           a.CreatedDate,
           a.CreatedBy,
           CreatedByName = _context.DataRoomMasterUsers
                               .Where(u => u.Id == a.CreatedBy &&  u.IsDeleted == false)
                               .Select(u => u.Name) 
                               .FirstOrDefault()
       })
       .ToListAsync();
            return Ok(new
            {
                DataRoom = dataRoom,
                Files = fileDetails,
                Permission = new
                {
                    UserPermission = userRoom.AccessLevel,
                    DataRoomPermission = dataRoom.DefaultPermission
                },
                AuditLogs = auditLogs
            });
        }








    }
}
