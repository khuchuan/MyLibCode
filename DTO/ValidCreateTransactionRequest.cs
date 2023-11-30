namespace DTO
{
    public class ValidCreateTransactionRequest
    {
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string Partner { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string ServiceId { get; set; }
        public string ProductCode { get; set; }
        public string BillingCode { get; set; }
        public string TrackingId { get; set; }
        public string PackCode { get; set; }
        public string? Extend1 { get; set; }
        public string? Extend2 { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public int Quantity { get; set; }
        public int? PromotionCampaignId { get; set; }
    }
}
