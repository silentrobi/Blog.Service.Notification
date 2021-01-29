using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			SmtpPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD", EnvironmentVariableTarget.User);
		}
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; }
	}
}
