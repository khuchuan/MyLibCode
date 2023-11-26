using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TransactionInfo
    {
        public string? Id { get; set; }
        public string? TrackingId { get; set; }
        public string? CustomerId { get; set; }
        public int ActualAmount { get; set; }
        public int Amount { get; set; }
        public string? Partner { get; set; }
        public string? ServiceId { get; set; }
        public string? ProductCode { get; set; }
        public string? PackCode { get; set; }
        public string? BillingCode { get; set; }
        public int Status { get; set; }
        public int? SendMail { get; set; }
        public string? ProductName { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Quantity { get; set; }
    }
    public class TransactionHistoryInfo : TransactionInfo
    {
        public DateTime ResponseTime { get; set; }
        public string? Username { get; set; }
        public string? ResultCode { get; set; }
        public int TotalRows { get; set; }
    }

    public class TransactionDetailInfo : TransactionInfo
    {
        public string? DiscountInfo { get; set; }
        public string? Extend1 { get; set; }
        public string? Extend2 { get; set; }
        public string? FundingSource { get; set; }
        public int TotalRows { get; set; }
    }
}
