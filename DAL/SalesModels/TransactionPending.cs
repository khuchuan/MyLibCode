using System;
using System.Collections.Generic;

namespace DAL.SalesModels
{
    public partial class TransactionPending
    {
        public string Id { get; set; }
        public string TrackingId { get; set; }
        public DateTime CreateTime { get; set; }
        public string ClientIp { get; set; }
        public string BillingCode { get; set; }
        public string ProductId { get; set; }
        public string PackId { get; set; }
        public decimal Amount { get; set; }
        public decimal ActualAmount { get; set; }
        public string Username { get; set; }
        public decimal DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public string ServiceId { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; }
        public string Partner { get; set; }
        public int ResultStatus { get; set; }
        public string ResultCode { get; set; }
        public DateTime? PayTime { get; set; }
        public DateTime? ResponseTime { get; set; }
        public DateTime? PartnerCreateTime { get; set; }
        public string FundingSource { get; set; }
        public string Extend1 { get; set; }
        public string Extend2 { get; set; }
        public int Quantity { get; set; }
        public int? PromotionCampaignId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
