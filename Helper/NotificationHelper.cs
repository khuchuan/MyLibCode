using DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Helper
{
    public class NotificationHelper
    {
        private readonly ILogger<NotificationHelper> _logger;
        private NotifyConfig _notifyConfig;
        private readonly EmailHelper _emailHelper;
        public NotificationHelper(IOptionsMonitor<NotifyConfig> notifyConfig, ILogger<NotificationHelper> logger, EmailHelper emailHelper)
        {
            _logger = logger;
            _emailHelper = emailHelper;
            _notifyConfig = notifyConfig.CurrentValue;
            notifyConfig.OnChange(value =>
            {
                _notifyConfig = value;
            });
        }
        public async Task<bool> SendAsync(Notification item, List<Attachment>? attachments = null)
        {
            if (_notifyConfig.Mode == NotifyMode.Email)
            {
                return await _emailHelper.SendEmailAsync(_notifyConfig.FromEmail, _notifyConfig.DisplayName, _notifyConfig.FromPassword, _notifyConfig.SmtpServer, _notifyConfig.SmtpPort, _notifyConfig.ToEmails, item, attachments);
            }
            return true;
        }
    }
}
