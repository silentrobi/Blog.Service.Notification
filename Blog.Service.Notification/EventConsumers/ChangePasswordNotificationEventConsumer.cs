using Blog.Service.BlogApi.Domain.Repositories;
using Blog.Service.Notification.Application.Services.EmailService;
using Blog.Service.Notification.Domain.Notification;
using MassTransit;
using SendGrid.Helpers.Mail;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Notification.Application.EventConsumers
{
    public class ChangePasswordNotificationEventConsumer : IConsumer<ChangePasswordNotification>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public ChangePasswordNotificationEventConsumer(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;

        }

        public async Task Consume(ConsumeContext<ChangePasswordNotification> context)
        {
            var eventData = context.Message;

            _emailService.SendEmail(new EmailMessage
            {
                ToAddresses = { new EmailAddress(eventData.Email) },
                Subject = eventData.Title,
                Content = HtmlTemplate.GetTemplate(eventData.Title, eventData.Message)
            });

            _unitOfWork.NotificationLogRepository.Create(
                new NotificationLog
                {
                    Type = "Change Password Notification",
                    Name = "Account password change"
                });

            _unitOfWork.Commit();
        }
    }
}
