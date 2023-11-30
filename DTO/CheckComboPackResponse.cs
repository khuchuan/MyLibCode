using System;
using System.Collections.Generic;

namespace DTO
{
    public class CheckComboPackResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string ServiceId { get; set; }
        public string BillingCode { get; set; }
        public string ProductCode { get; set; }
        public string TransactionId { get; set; }
        public decimal DiscountRate { get; set; }
        public CurrentPack CurrentPack { get; set; }
        public IEnumerable<ComboPack> Packs { get; set; }
    }
    public class CurrentPack
    {
        public string PackName { get; set; }
        public string PackCode { get; set; }
        public string Description { get; set; }
        public DateTime? PackExpiry { get; set; }
        public string RemainData { get; set; }
        public string TotalData { get; set; }
    }

    public class ComboPack
    {
        public string PackName { get; set; }
        public string PackCode { get; set; }
        public int PackAmount { get; set; }
        public string PackData { get; set; }
        public int PackTimeLimit { get; set; }
        public string PackType { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public string PromotionTimeLimit { get; set; }
        public string Detail { get; set; } //Mô tả chi tiết gói
    }
}
