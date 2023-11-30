using System;
using System.Collections.Generic;

namespace DAL.SalesModels
{
    public partial class RefundOrder_View
    {
        public long? Rank { get; set; }
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string TrackingId { get; set; }
        public DateTime CreateTime { get; set; }
        public string BillingCode { get; set; }
        public decimal ActualAmount { get; set; }
        public int ResultStatus { get; set; }
        public DateTime? PartnerCreateTime { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string ResultCode { get; set; }
        public string UserName { get; set; }
        public string PartnerRefundId { get; set; }
    }
}
