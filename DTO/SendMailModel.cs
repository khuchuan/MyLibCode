namespace DTO
{
    public class SendMailModel
    { 
        public string ToEmail { get; set; } = string.Empty;
        public string EmailTitle { get; set; } = string.Empty;
        public string EmailContent { get; set; } = string.Empty;
        public string Callback { get; set; } = string.Empty; 
        public string TrackingId { get; set; } = string.Empty;
    }
}
