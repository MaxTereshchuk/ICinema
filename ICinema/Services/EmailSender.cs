using System.Net;
using System.Net.Mail;
using ICinema.Interfaces;
using ICinema.Models;

namespace ICinema.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
            var client =  new SmtpClient(emailSettings.SmtpServer, emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword),
                EnableSsl = true
            };
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true

            };
            mailMessage.To.Add(email);
            try
            {
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
