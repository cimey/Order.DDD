using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Consumer
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {
        public Task SendAsync(string to, string subject, string body)
        {
            // Here you would implement the actual email sending logic.
            // For example, using SMTP client or any email service provider's API.
            Console.WriteLine($"Sending email to: {to}, Subject: {subject}, Body: {body}");
            return Task.CompletedTask;
        }
    }
}
