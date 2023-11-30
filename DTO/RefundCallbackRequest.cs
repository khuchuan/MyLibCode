using System;

namespace DTO
{
    public class RefundCallbackRequest
    {
        public string? RefundId { get; set; }
        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string? PartnerStatus { get; set; }
        public string? Checksum { get; set; }
        public string? Partner { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? ResultCode { get; set; }
        public int ResultStatus { get; set; }
    }
}
