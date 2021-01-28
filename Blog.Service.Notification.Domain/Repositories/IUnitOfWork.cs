using Blog.Service.Notification.Domain.Notification;

namespace Blog.Service.Notification.Domain.Repositories
{
    public interface IUnitOfWork
    {
        INotificationLogRepository NotificationLogRepository { get; }

        void Commit();
    }
}