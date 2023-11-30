using System;
using System.Collections.Generic;

namespace DAL.SalesModels
{
    public partial class PromotionDetail
    {
        public int Id { get; set; }
        public int PromotionCampaignId { get; set; }
        public string ServiceId { get; set; }
        public string ProductId { get; set; }
        public string PackId { get; set; }
        public decimal DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public int? QuantityPerAccount { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int? MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public int? TotalPerAccount { get; set; }
        public int? TotalAmount { get; set; }
    }
}
