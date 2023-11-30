using System;

namespace DTO
{
    public class ProcessTransactionRequest
    {
        public string? TransactionId { get; set; }
        //public string? MerchantCode { get; set; }
        //public string? MerchantName { get; set; }
        public string? CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        //public string? TypeCode { get; set; }
        //public string? TypeName { get; set; }
        //public bool? TypeAllowCard { get; set; }
        //public string? SuccessMessage { get; set; }
        //public string? Metadata { get; set; }
        //public DateTime? CreatedTime { get; set; }
        public DateTime? PaidTime { get; set; }
        //public string? PartnerStatus { get; set; }
        public string? FundingSource { get; set; }
        public string? Checksum { get; set; }
        public string? Partner { get; set; }
        public int Status { get; set; }
        public string? OriginText { get; set; }
    }
}
