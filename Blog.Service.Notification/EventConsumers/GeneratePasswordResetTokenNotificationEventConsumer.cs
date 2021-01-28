using Blog.Service.Notification.Application.Services.EmailService;
using Blog.Service.Notification.Domain.Notification;
using Blog.Service.Notification.Domain.Repositories;
using MassTransit;
using SendGrid.Helpers.Mail;
using SharedLibrary;
using System.Threading.Tasks;

namespace Blog.Service.Notification.Application.EventConsumers
{
    public class GeneratePasswordResetTokenNotificationEventConsumer : IConsumer<GeneratePasswordResetTokenNotification>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public GeneratePasswordResetTokenNotificationEventConsumer(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;

        }
        public async Task Consume(ConsumeContext<GeneratePasswordResetTokenNotification> context)
        {
            var eventData = context.Message;

            _emailService.SendEmail(new EmailMessage
            {
                ToAddresses = { new EmailAddress(eventData.Email) },
                Subject = eventData.Title,
                Content = HtmlTemplate.GetTemplate(eventData.Title, eventData.Message + $" <b>{eventData.Token}</b>")
            });

            _unitOfWork.NotificationLogRepository.Create(
                new NotificationLog
                {
                    Type = "Generate Password ResetCode Notification",
                    Name = "Password recovery"
                });

            _unitOfWork.Commit();
        }
    }
}
