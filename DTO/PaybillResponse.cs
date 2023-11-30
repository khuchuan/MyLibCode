namespace DTO
{
    public class PaybillResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public string ServiceId { get; set; }
        public string ProductCode { get; set; }
        public string BillingName { get; set; }
        public string BillingCode { get; set; }
        public decimal Amount { get; set; }
        public decimal ActualAmount { get; set; }
        public string PackCode { get; set; }
    }
}
