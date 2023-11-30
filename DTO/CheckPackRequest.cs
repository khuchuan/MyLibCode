namespace DTO
{
    public class CheckPackRequest
    {
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string BillingCode { get; set; }
        public string ProductCode { get; set; }
        public string ServiceId { get; set; }
        public string Signature { get; set; }
    }
}
