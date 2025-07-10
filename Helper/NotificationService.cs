using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;

namespace OISPublic.Helper
{
    public class NotificationService
    {
        private readonly OISDataRoomContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(OISDataRoomContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<List<DataRoomNotification>> SendUserNotificationsAsync(Guid userId)
        {
            var notifications = await _context.DataRoomNotifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            await _hubContext.Clients.Group(userId.ToString()).SendAsync("NotificationUpdated", new
            {
                Count = notifications.Count,
                Timestamp = DateTime.Now // using local time instead of UTC
            });

            return notifications;
        }
    }
}
