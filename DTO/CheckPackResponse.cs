using System.Collections.Generic;

namespace DTO
{
    public class CheckPackResponse
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? ServiceId { get; set; }
        public string? BillingCode { get; set; }
        public string? ProductCode { get; set; }
        public string? TransactionId { get; set; }
        public decimal DiscountRate { get; set; }
        public IEnumerable<Pack>? Packs { get; set; }
    }
}
