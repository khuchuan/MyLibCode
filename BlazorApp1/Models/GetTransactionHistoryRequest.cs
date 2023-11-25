
namespace BlazorApp1.Models
{
    public class GetTransactionHistoryRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Service { get; set; }
        public string CustomerCode { get; set; }
        public int? PayBillStatus { get; set; }
        public string TransactionId { get; set; }
        public string BillingCode { get; set; }
        public string Partner { get; set; }
        public int? SendMail { get; set; }
        public string SortExpr { get; set; }
        public string SortDir { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }



}
