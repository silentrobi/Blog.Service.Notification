using Blog.Service.BlogApi.Domain.Repositories;
using Blog.Service.BlogApi.Infrastructure.Repositories;
using Blog.Service.Notification.Application.EventConsumers;
using Blog.Service.Notification.Application.Services.EmailService;
using Blog.Service.Notification.Domain.Notification;
using Blog.Service.Notification.Infrastructure.Database.Contexts;
using Blog.Service.Notification.Infrastructure.Domain.Notification;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Blog.Service.NotificationSerder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NotificationDbContext>(
               options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<TestEventConsumer>();
                x.AddConsumer<GeneratePasswordResetTokenNotificationEventConsumer>();
                x.AddConsumer<AccountCreateNotificationEventConsumer>();
                x.AddConsumer<AccountPasswordResetNotificationEventConsumer>();
                x.AddConsumer<ChangePasswordNotificationEventConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(
                        new Uri("rabbitmq://localhost"), h =>
                        {
                            h.Username("user");
                            h.Password("user");
                        });
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();
            services.AddControllers();

            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<INotificationLogRepository,NotificationLogRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AutoMigrate(app);
        }

        private void AutoMigrate(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<NotificationDbContext>();

            context.Database.Migrate();
        }
    }
}
