using System;

namespace SharedLibrary
{
    public abstract class Notification
    {
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }
}
