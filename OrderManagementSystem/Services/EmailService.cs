using System.Threading.Tasks;

namespace OrderManagementSystem.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            return Task.CompletedTask;
        }
    }
}