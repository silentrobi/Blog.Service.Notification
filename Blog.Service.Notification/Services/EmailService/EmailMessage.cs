using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace Blog.Service.Notification.Application.Services.EmailService
{
    public class EmailMessage
    {
		public EmailMessage()
		{
			ToAddresses = new List<EmailAddress>();
			FromAddress = new EmailAddress();
		}

		public List<EmailAddress> ToAddresses { get; set; }
		public EmailAddress FromAddress { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}
