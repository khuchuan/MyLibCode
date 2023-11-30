using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RefundOrderViewModel
    {
        public string? Id { get; set; }
        public string? TrackingId { get; set; }
        public string? BillingCode { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime CreateTime { get; set; }
        public int ResultStatus { get; set; }
        public int? RefundResultStatus { get; set; }
        public DateTime? RefundCreateTime { get; set; }
        public DateTime? RefundResponseTime { get; set; }
        public int TotalRows { get; set; }
    }
}
