﻿namespace ICinema.Interfaces
{
    public interface IEmailSender
    {
        public Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}
