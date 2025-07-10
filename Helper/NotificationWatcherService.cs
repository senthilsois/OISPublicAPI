using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;

namespace OISPublic.Helper
{
    public class NotificationWatcherService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationWatcherService> _logger;

        private DateTime _lastCheck = DateTime.UtcNow;

        public NotificationWatcherService(IServiceProvider serviceProvider, IHubContext<NotificationHub> hubContext, ILogger<NotificationWatcherService> logger)
        {
            _serviceProvider = serviceProvider;
            _hubContext = hubContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<OISDataRoomContext>();
                var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();

                var newNotifications = await dbContext.DataRoomNotifications
                    //.Where(n => n.CreatedAt > _lastCheck)
                    .ToListAsync();

                var grouped = newNotifications.GroupBy(n => n.UserId);
                foreach (var group in grouped)
                {
                    if (group.Key.HasValue)
                    {
                        await notificationService.SendUserNotificationsAsync(group.Key.Value);
                    }

                    _logger.LogInformation($"Notification triggered for user {group.Key}");
                }

                _lastCheck = DateTime.Now;
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

    }

}
