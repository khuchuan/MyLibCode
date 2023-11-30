using System;
using System.Collections.Generic;

namespace DAL.SalesModels
{
    public partial class PromotionCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SubTimeRange { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UserName { get; set; }
        public int? QuantityPerDay { get; set; }
        public int? QuantityPerAccount { get; set; }
        public int IsActive { get; set; }
        public string Description { get; set; }
        public int? Type { get; set; }
        public string Code { get; set; }
        public int? TotalAmount { get; set; }
        public int? AlertAmount { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public int? TotalPerAccount { get; set; }
    }
}
