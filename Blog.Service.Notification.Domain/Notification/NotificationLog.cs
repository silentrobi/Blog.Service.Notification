using System;

namespace Blog.Service.Notification.Domain.Notification
{
    public class NotificationLog
    {
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
