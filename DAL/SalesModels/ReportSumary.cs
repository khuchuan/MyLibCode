using System;

#nullable disable

namespace DAL.SalesModels
{
    public partial class ReportSummary
    {
        public long ID { get; set; }
        public DateTime Day { get; set; }
        public string Partner { get; set; }
        public string Service { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int TotalRows { get; set; }
    }
}
