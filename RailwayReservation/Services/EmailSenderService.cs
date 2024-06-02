using RailwayReservation.Interface.Service;
using System.Net.Mail;

namespace RailwayReservation.Services
{
    /// <summary>
    /// Service class for sending emails.
    /// </summary>
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The content of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("Email");
            var useremail = emailSettings["mail"];
            var password = emailSettings["password"];

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(useremail, password),
                EnableSsl = true,
            };

            // Use the retrieved email and password to send the email

            return client.SendMailAsync(
                               new MailMessage(useremail, email, subject, message)
                               {
                                   IsBodyHtml = true
                               }
                                        );
        }

    }
}
