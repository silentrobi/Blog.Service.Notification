using Blog.Service.BlogApi.Domain.Repositories;
using Blog.Service.Notification.Domain.Notification;
using Blog.Service.Notification.Infrastructure.Database.Contexts;

namespace Blog.Service.BlogApi.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NotificationDbContext _dbContext;
        public UnitOfWork(
        INotificationLogRepository notificationLogRepository,
        NotificationDbContext dbContext)
        {
            NotificationLogRepository = notificationLogRepository;
            _dbContext = dbContext;
        }

        public INotificationLogRepository NotificationLogRepository { get; }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}