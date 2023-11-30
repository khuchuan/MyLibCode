using System;

namespace DTO
{

    public class ReportRevenueModel
    {
        public string? Service { get; set; }
        public string? Product { get; set; }
        public decimal Statistic { get; set; }

        public string? StatisticString
        {
            get; set;
        }
        public decimal StatisticActualAmount { get; set; }

        public string? StatisticActualAmountString
        {
            get; set;
        }
        public string? Ratio { get; set; }

        public decimal StatisticFormatMillion
        {
            get
            {
                return Math.Round(Statistic / 1000000, 2);
            }
        }
        public decimal StatisticFormatBillion
        {
            get
            {
                return Math.Round(Statistic / 1000000000, 2);
            }
        }
    }
}
