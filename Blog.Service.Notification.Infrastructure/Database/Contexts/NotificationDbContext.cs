using Microsoft.EntityFrameworkCore;
using Blog.Service.Notification.Domain.Notification;
namespace Blog.Service.Notification.Infrastructure.Database.Contexts
{
    public class NotificationDbContext : DbContext
    {
        public DbSet<NotificationLog> NotificationLog { get; set; }

        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            base.OnModelCreating(optionsBuilder);

            optionsBuilder.HasPostgresExtension("uuid-ossp");

            optionsBuilder.Entity<NotificationLog>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("uuid_generate_v4()");
                b.Property(u => u.Date).HasDefaultValueSql("now()");
            });
        }
    }
}
