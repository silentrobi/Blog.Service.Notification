namespace Blog.Service.Notification.Application.Services.EmailService
{
    public class HtmlTemplate
    {
        public static string GetTemplate(string title, string message)
        {
            return @$"<html>
                      <body>
                      <p>{title}</p>
                      <p>{message}</p>
                      </body>
                      </html>
                     ";
        }
    }
}
