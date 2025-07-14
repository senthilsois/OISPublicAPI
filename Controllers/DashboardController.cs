using DocumentManagement.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;
using OISPublic.Services;

namespace OISPublic.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {

        private readonly OISDataRoomContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly PathHelper _pathHelper;
        private IWebHostEnvironment _webHostEnvironment;
        public DashboardController(

           IWebHostEnvironment webHostEnvironment,
           OISDataRoomContext context,
           IConfiguration configuration,
           JwtTokenService jwtTokenService,
           PathHelper pathHelper

         )
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _pathHelper = pathHelper;


        }


        public class RecentActivityDto
        {
            public string FileName { get; set; }
            public string Action { get; set; }
            public DateTime ActionDate { get; set; }
        }

        public class UserDashboardDto
        {
            public int TotalDataRooms { get; set; }
            public int TotalUploadedFiles { get; set; }
            public int TotalDocumentViews { get; set; }
            public int PendingApprovals { get; set; }
            public int RecentNotifications { get; set; }
            public List<Document> RecentUploadedFileNames { get; set; }
            public List<RecentActivityDto> RecentActivities { get; set; }
            public List<DataRoom> ExpiredDataRooms { get; set; }

        }








        [HttpGet("getUserDashboardData")]
        public async Task<IActionResult> GetUserDashboardData([FromQuery] Guid userId)
        {
            try
            {
                var userExists = await _context.DataRoomUsers.AnyAsync(u => u.UserId == userId);
                if (!userExists)
                {
                    return NotFound("User not found.");
                }

                var userDataRoomIds = await _context.DataRoomUsers
                    .Where(u => u.UserId == userId)
                    .Select(u => u.DataRoomId)
                    .Distinct()
                    .ToListAsync();

                var totalDataRooms = await _context.DataRooms
                    .CountAsync(dr => userDataRoomIds.Contains(dr.Id));

                var accessibleFiles = await _context.DataRoomFiles
                    .Where(f => userDataRoomIds.Contains(f.DataRoomId))
                    .ToListAsync();

                var accessibleDocumentIds = accessibleFiles
                    .Select(f => f.DocumentId)
                    .Distinct()
                    .ToList();

                var totalViews = await _context.Documents
                    .Where(d => accessibleDocumentIds.Contains(d.Id))
                    .SumAsync(d => d.ViewCount);

                var notifications = await _context.DataRoomNotifications
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedAt)
                    .Take(5)
                    .ToListAsync();

                var notificationCount = notifications.Count;

            
                var uploadedFiles = accessibleFiles
                    .Where(f => f.UploadedBy == userId)
                    .ToList();

                var recentUploads = uploadedFiles
                    .OrderByDescending(f => f.UploadedAt)
                    .Take(5)
                    .ToList();

                var recentUploadDocumentIds = recentUploads.Select(f => f.DocumentId).ToList();

                var recentUploadDocuments = await _context.Documents
                    .Where(d => recentUploadDocumentIds.Contains(d.Id))
                    .ToListAsync();

                var recentUploadedFileNames = recentUploadDocuments;

              
                var recentActivityLogs = await _context.DataRoomAuditTrials
                    .Where(at => at.UserId == userId && !string.IsNullOrEmpty(at.DocumentName))
                    .OrderByDescending(at => at.CreatedDate)
                    .Take(10)
                    .ToListAsync();

                var documentNames = recentActivityLogs
                    .Select(a => a.DocumentName)
                    .Distinct()
                    .ToList();

                var documentMap = await _context.Documents
                    .Where(d => documentNames.Contains(d.Name))
                    .ToDictionaryAsync(d => d.Name, d => d.Url);

                var recentActivities = recentActivityLogs
                    .Where(a => !string.IsNullOrEmpty(a.DocumentName))
                    .Select(a =>
                    {
                        string extension = "";

                        if (documentMap.TryGetValue(a.DocumentName, out var url) && !string.IsNullOrEmpty(url))
                        {
                            extension = Path.GetExtension(url); 
                        }

                        return new RecentActivityDto
                        {
                            FileName = $"{a.DocumentName}{extension}",
                            Action = a.ActionName,
                            ActionDate = a.CreatedDate ?? DateTime.MinValue
                        };
                    })
                    .OrderByDescending(r => r.ActionDate)
                    .Take(5)
                    .ToList();

              
                var expiredDataRooms = await _context.DataRooms
                    .Where(dr => userDataRoomIds.Contains(dr.Id) && dr.ExpirationDate != null && dr.ExpirationDate < DateTime.UtcNow)
                    .ToListAsync();


              
                var result = new UserDashboardDto
                {
                    TotalDataRooms = totalDataRooms,
                    TotalUploadedFiles = uploadedFiles.Count,
                    TotalDocumentViews = totalViews,
                    PendingApprovals = 0, 
                    RecentNotifications = notificationCount,
                    RecentUploadedFileNames = recentUploadedFileNames,
                    RecentActivities = recentActivities,
                    ExpiredDataRooms = expiredDataRooms
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
