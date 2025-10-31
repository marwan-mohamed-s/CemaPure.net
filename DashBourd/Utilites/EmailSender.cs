using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace DashBourd.Utilites
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mmsm34567890@gmail.com", "hidx wpyv kwuh snsx")
            };

            return client.SendMailAsync(
            new MailMessage(from: "mmsm34567890@gmail.com",
                            to: email,
                            subject,
                            htmlMessage
                            )
            {
                IsBodyHtml = true
            });
        }
    }
}
