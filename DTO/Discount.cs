namespace DTO
{
    public class Discount
    {
        public int PromotionCampaignId { get; set; } = 0;
        public int DiscountType { get; set; } = 0;
        public decimal DiscountValue { get; set; } = 0;
        public int ActualAmount { get; set; } = 0;
        public string? DiscountInfo { get; set; }
    }
}
