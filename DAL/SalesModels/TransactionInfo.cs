using System;

namespace DAL.SalesModels
{
    public partial class TransactionInfo
    {
        public string Id { get; set; }
        public decimal ActualAmount { get; set; }
        public string Partner { get; set; }
        public string ServiceId { get; set; }
        public string ProductCode { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Status { get; set; }
        public string ProductName { get; set; }
        public int TotalRows { get; set; }
    }
    public partial class TransactionHistoryInfo
    {
        public string Id { get; set; }
        public string TrackingId { get; set; }
        public string Partner { get; set; }
        public string BillingCode { get; set; }

        public decimal Amount { get; set; }
        public decimal ActualAmount { get; set; }
        public string Username { get; set; }
        public string ServiceId { get; set; }
        public string ProductCode { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Status { get; set; }
        public string ResultCode { get; set; }
        public int TotalRows { get; set; }
        public DateTime ResponseTime { get; set; }
    }
}
