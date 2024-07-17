using System.Net;
using System.Net.Mail;
using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using Microsoft.EntityFrameworkCore;

namespace ICinema.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _appDBContext;
        public EmailSender(IConfiguration configuration, AppDBContext appDBContext)
        {
            _configuration = configuration;
            _appDBContext = appDBContext;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            var emailSettings =await _appDBContext.EmailSettings.FirstOrDefaultAsync(e=>e.Id==1);
            if(emailSettings == null) 
                return false;

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
