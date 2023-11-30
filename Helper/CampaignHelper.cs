
using System.Text.Json;

namespace Helper
{
    public class CampaignHelper
    {
        public static int GetStatusCampaign(DAL.SalesModels.PromotionCampaign campagin)
        {
            var subTimeRangeLast = new SubTimeRange();
            var subTimeRangeFirst = new SubTimeRange();
            var dateNow = DateTime.Now.Date;
            if (string.IsNullOrEmpty(campagin.SubTimeRange))
            {
                subTimeRangeFirst.StartTime = "00:00";
                subTimeRangeFirst.EndTime = "23:59:59";
                subTimeRangeLast = subTimeRangeFirst;
            }
            else
            {
                subTimeRangeFirst = JsonSerializer.Deserialize<List<SubTimeRange>>(campagin.SubTimeRange)?.FirstOrDefault();
                subTimeRangeLast = JsonSerializer.Deserialize<List<SubTimeRange>>(campagin.SubTimeRange)?.LastOrDefault();
            }

            if(campagin.IsActive == Constants.PromotionCampaign.Active.Tat)
            {
                return Constants.PromotionCampaign.Status.Tat;
            }

            if (campagin.StartTime.Date > dateNow
                || (campagin.StartTime.Date == dateNow && DateTimeFromTimeOnly(campagin.StartTime.Date, subTimeRangeFirst.StartTime) > DateTime.Now))
            {
                return Constants.PromotionCampaign.Status.ChuaApDung;
            }
            else if ((campagin.StartTime.Date == dateNow || DateTimeFromTimeOnly(campagin.StartTime.Date, subTimeRangeFirst.StartTime) < DateTime.Now)
                && (campagin.EndTime.Date > dateNow || DateTimeFromTimeOnly(campagin.EndTime.Date, subTimeRangeLast.EndTime) > DateTime.Now))
            {
                return Constants.PromotionCampaign.Status.DangApDung;
            }
            else if (campagin.EndTime.Date < dateNow || (campagin.EndTime.Date == dateNow && DateTimeFromTimeOnly(campagin.EndTime.Date, subTimeRangeLast.EndTime) < DateTime.Now))
            {
                return Constants.PromotionCampaign.Status.DaApDung;
            }
            return campagin.IsActive;
        }
        private static DateTime? DateTimeFromTimeOnly(DateTime date, string? timeOnly)
        {
            if (string.IsNullOrEmpty(timeOnly))
            {
                return null;
            }
            var dateString = date.ToShortDateString();
            DateTime.TryParse($"{dateString} {timeOnly}", out var today);

            return today;
        }
        private class SubTimeRange
        {
            public string? StartTime { get; set; }

            public string? EndTime { get; set; }
        }
    }
}
