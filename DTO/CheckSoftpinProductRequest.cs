namespace DTO
{
    public class CheckSoftpinProductRequest
    {
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string ServiceCode { get; set; }
        public string Signature { get; set; }
    }
}
