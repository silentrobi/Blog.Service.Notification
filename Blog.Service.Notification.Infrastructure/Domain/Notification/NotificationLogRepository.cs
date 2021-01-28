using Blog.Service.Notification.Infrastructure.Database.Contexts;
using Blog.Service.Notification.Domain.Notification;

namespace Blog.Service.Notification.Infrastructure.Domain.Notification
{
    public class NotificationLogRepository : INotificationLogRepository
    {
        private readonly NotificationDbContext _context;

        public NotificationLogRepository(NotificationDbContext context)
        {
            _context = context;
        }

        public void Create(NotificationLog notificationLog)
        {
            _context.NotificationLog.Add(notificationLog);
        }
    }
}
