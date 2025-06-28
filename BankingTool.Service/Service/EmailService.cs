using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BankingTool.Service.Service
{
    public class EmailService
    {
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", "yourgmail@gmail.com"));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Use your Gmail address and App Password
                await client.AuthenticateAsync("yourgmail@gmail.com", "your_app_password");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
    }
}
