namespace DTO
{
    public class PaybillConfig
    {
        public const string Name = "Paybill";
        public string? Url { get; set; }
        public int TimeoutInSeconds { get; set; }
        public AuthenConfig Authen { get; set; }
    }
    public class CardConfig
    {
        public const string Name = "Card";
        public string? Url { get; set; }
        public int TimeoutInSeconds { get; set; }
        public AuthenConfig Authen { get; set; }
    }
    public class TopupConfig
    {
        public const string Name = "Topup";
        public string? Url { get; set; }
        public int TimeoutInSeconds { get; set; }
        public string? UserId { get; set; }
    }
    public class AuthenConfig
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public string PrivateKey { get; set; }
    }

    public class AuthenValidationConfig
    {
        public const string Name = "AuthenValidation";
        public string TokenUrl { get; set; }
        public string TokenIssuer { get; set; }
        public string AudienceClientId { get; set; }
        public string AudienceBase64Secret { get; set; }
    }

    public class NotifyConfig
    {
        public const string Name = "Notify";
        public int Mode { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string FromPassword { get; set; } = string.Empty;
        public string ToEmails { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
    }


}
