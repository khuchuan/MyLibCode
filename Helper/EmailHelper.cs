using DTO;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Helper
{
    public class EmailHelper
    {
        private readonly ILogger<EmailHelper> _logger;
        public EmailHelper(ILogger<EmailHelper> logger)
        {
            _logger = logger;
        }
        public async Task<bool> SendEmailAsync(string fromEmail, string displayName, string fromPassword, string smtpServer, int smtpPort, string toEmails, Notification item, List<Attachment>? attachments = null)
        {
            try
            {
                using var mail = new MailMessage
                {
                    From = new MailAddress(fromEmail, displayName),
                };
                string[] mailList = toEmails.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var email in mailList)
                {
                    mail.To.Add(new MailAddress(email));
                }
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(item.Message, Encoding.UTF8, MediaTypeNames.Text.Html));
                mail.Subject = item.EmailTitle;
                if (attachments != null)
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }
                var credentials = new System.Net.NetworkCredential(fromEmail, fromPassword);
                using var client = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = credentials,
                };
                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmail Exception, Request: {request}", JsonSerializer.Serialize(item));
                return false;
            }
        }
    }
}
