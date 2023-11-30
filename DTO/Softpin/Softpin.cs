using System.Collections.Generic;

namespace DTO.Softpin
{
    public class SoftpinTransactionResponse
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? TransactionId { get; set; }
        public SoftpinResponseModel? SoftpinResult { get; set; }
    }


    public class SoftpinResponseModel
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? TransactionId { get; set; }
        public int Status { get; set; }
        public IEnumerable<Softpin>? Softpins { get; set; }
    }

    public class Softpin
    {
        public string? PinCode { get; set; }
        public string? Serial { get; set; }
        public string? ExpiryDate { get; set; }
    }
}
