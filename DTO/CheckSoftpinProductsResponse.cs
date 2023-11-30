using System.Collections.Generic;

namespace DTO
{
    public class CheckSoftpinProductsResponse
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? ServiceCode { get; set; }
        public string? TransactionId { get; set; }
        public IEnumerable<SoftpinProduct>? Products { get; set; }
    }

    public class SoftpinProduct
    {

        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public IEnumerable<Pack>? Packs { get; set; }
    }
    //public class SoftpinPack
    //{
    //    public string? PackName { get; set; }
    //    public string? PackCode { get; set; }
    //    public int PackAmount { get; set; }
    //}
}
