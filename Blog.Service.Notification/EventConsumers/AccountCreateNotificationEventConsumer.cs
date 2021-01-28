using Blog.Service.Notification.Application.Services.EmailService;
using Blog.Service.Notification.Domain.Notification;
using Blog.Service.Notification.Domain.Repositories;
using MassTransit;
using SendGrid.Helpers.Mail;
using SharedLibrary;
using System.Threading.Tasks;

namespace Blog.Service.Notification.Application.EventConsumers
{
    public class AccountCreateNotificationEventConsumer : IConsumer<AccountCreateNotification>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public AccountCreateNotificationEventConsumer(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<AccountCreateNotification> context)
        {
            var eventData = context.Message;

            _emailService.SendEmail(new EmailMessage { 
                ToAddresses = { new EmailAddress(eventData.Email) },
                Subject = eventData.Title,
                Content = HtmlTemplate.GetTemplate(eventData.Title, eventData.Message)
            });

            _unitOfWork.NotificationLogRepository.Create(
                new NotificationLog {
                    Type = "Account Create Notification",
                    Name = "Account Create Notification"
                });

            _unitOfWork.Commit();
        }
    }
}
