namespace DTO
{
    public class PaybillRequest
    {
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string Signature { get; set; }
        public string BillingCode { get; set; }
        public decimal Amount { get; set; }
        public string ProductCode { get; set; }
        public string ServiceId { get; set; }
        public string PackCode { get; set; }
    }
}
