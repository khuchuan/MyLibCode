using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Helper;
using Newtonsoft.Json;

namespace MapperConfig;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //CreateMap<Timestamp, DateTime>().ConvertUsing(x => DateTimeFormat.ToDateTime(x));
        //CreateMap<Timestamp, DateTime?>().ConvertUsing(x => DateTimeFormat.ToDateTime(x));
        //CreateMap<DateTime, Timestamp>().ConvertUsing(x => DateTimeFormat.ToTimestamp(x));
        //CreateMap<DateTime?, Timestamp>().ConvertUsing(x => DateTimeFormat.ToTimestamp(x));

        #region SalesService
        CreateMap<SalesService.Pack, DTO.Pack>().ReverseMap();
        CreateMap<DTO.CheckPackResponse, SalesService.GetPacksResponse>().ReverseMap();
        CreateMap<DTO.CustomerInfo, SalesService.CustomerInfo>().ReverseMap();
        CreateMap<DTO.TransactionDetailInfo, SalesService.TransactionDetail>().ReverseMap();
        CreateMap<DAL.SalesModels.Transaction, SalesService.TransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.Transaction, DAL.SalesModels.TransactionPending>().ReverseMap();
        CreateMap<DAL.SalesModels.TransactionPending, SalesService.TransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.RequestSupport, SalesService.RequestSupportItem>().ReverseMap();

        CreateMap<DTO.TransactionHistoryInfo, SalesService.TransactionInfo>().ReverseMap();
        CreateMap<DTO.TransactionInfo, SalesService.TransactionInfo>().ReverseMap();
        #endregion

        #region ReportService
        CreateMap<DTO.TransactionHistoryInfo, ReportService.TransactionHistoryItem>().ReverseMap();
        CreateMap<DAL.SalesModels.Transaction, ReportService.TransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.Transaction, ReportService.RequestSupportTransactionItem>().ReverseMap();
        CreateMap<ReportService.TransactionItem, ReportService.RequestSupportTransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.TransactionPending, ReportService.TransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.ReportSummary, ReportService.ReportSummaryItem>().ReverseMap();
        CreateMap<DTO.AuditLog, ReportService.AuditLogItem>().ReverseMap();
        CreateMap<DTO.TreeNode, ReportService.DetailItem>().ReverseMap();
        CreateMap<DAL.SalesModels.RequestSupport, ReportService.RequestSupportItem>().ReverseMap();
        CreateMap<DTO.RequestSupportInfo, ReportService.RequestSupportItem>().ReverseMap();
        CreateMap<DTO.ReportRevenueModel, ReportService.ReportRevenueItem>().ReverseMap();
        CreateMap<DTO.PaybillResponse, ReportService.PaybillResponseItem>().ReverseMap();
        CreateMap<DAL.SalesModels.Customer, ReportService.CustomerItem>().ReverseMap();
        CreateMap<DAL.SalesModels.RefundOrder, ReportService.RefundOrderItem>().ReverseMap();
        CreateMap<DAL.SalesModels.RefundOrder, ReportService.TransactionRefundOrderItem>().ReverseMap();
        CreateMap<DAL.SalesModels.PromotionCampaign, AdminService.PromotionCampaignItem>()
            .ForMember(x => x.Status, a => a.MapFrom(src => CampaignHelper.GetStatusCampaign(src)));
        CreateMap<AdminService.PromotionCampaignItem, DAL.SalesModels.PromotionCampaign>();

        CreateMap<DTO.RefundOrderViewModel, ReportService.TransactionRefundItem>().ReverseMap();

        #endregion
        CreateMap<DAL.SalesModels.Transaction, AdminService.TransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.TransactionPending, AdminService.TransactionItem>().ReverseMap();
        CreateMap<DAL.SalesModels.RequestSupport, AdminService.RequestSupportItem>().ReverseMap();
        CreateMap<DTO.Paybill.ProductInfo, AdminService.ProductItem>().ReverseMap();
        CreateMap<DTO.Paybill.PackInfo, AdminService.PacksItem>().ReverseMap();
        CreateMap<DAL.SalesModels.PromotionCampaign, AdminService.PromotionCampaignItem>()
            .ForMember(x => x.Status, a => a.MapFrom(src => CampaignHelper.GetStatusCampaign(src)));

        CreateMap<AdminService.PromotionCampaignItem, DAL.SalesModels.PromotionCampaign>();
        CreateMap<DAL.SalesModels.PromotionDetail, AdminService.PromotionConfigItem>().ReverseMap();

        CreateMap<AdminService.RefundOrderResult, SalesService.CreateRefundOrderResponse>().ReverseMap();



    }

}