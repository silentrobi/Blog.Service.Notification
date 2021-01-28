namespace Blog.Service.Notification.Domain.Notification
{
    public interface INotificationLogRepository
    {
        void Create(NotificationLog notificationLog);
    }
}
