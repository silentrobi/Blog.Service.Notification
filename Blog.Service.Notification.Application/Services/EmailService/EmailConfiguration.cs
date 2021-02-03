using System;

namespace Blog.Service.Notification.Application.Services.EmailService
{
    public interface IEmailConfiguration
    {
		string SmtpServer { get; }
		int SmtpPort { get; }
		string SmtpUsername { get; set; }
		string SmtpPassword { get; }
	}

	public class EmailConfiguration : IEmailConfiguration
	{
		public EmailConfiguration()
        {
			SmtpUsername = Environment.GetEnvironmentVariable("EMAIL_NAME");
			SmtpServer = Environment.GetEnvironmentVariable("EMAIL_SERVER");
			SmtpPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
		}

		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; }
	}
}
