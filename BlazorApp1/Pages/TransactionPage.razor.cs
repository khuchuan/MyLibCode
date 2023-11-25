using AntDesign;
using BlazorApp1.Models;
using Helper;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;


namespace BlazorApp1.Pages
{
    public  partial class TransactionPage
    {
        private static DateTime DefaultStartDate = DateTime.Today.AddDays(-7);
        private static DateTime DefaultEndDate = DateTime.Today;


        public class Model
        {
            public DateTime?[] RangePicker { get; set; } = new DateTime?[] { DefaultStartDate, DefaultEndDate };
            public string Service { get; set; } = string.Empty;
            public string Provider { get; set; } = string.Empty;
            public string CustomerCode { get; set; } = string.Empty;
            public string BillingName { get; set; } = string.Empty;
            public int? PayBillStatus { get; set; } = null;
            public int? SendMail { get; set; } = null;
            public string TransactionId { get; set; } = string.Empty;
            public string BillingCode { get; set; } = string.Empty;
            public string SortExpr = "CreateTime";
            public string SortDir = "DESC";
        }

        private GetTransactionHistoryRequest _request = new GetTransactionHistoryRequest()
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            Service = string.Empty,
            CustomerCode = string.Empty,
            Partner = string.Empty,
            PayBillStatus = 0,
            TransactionId = string.Empty,
            BillingCode = string.Empty,
            SortExpr = "CreateTime",
            SortDir = "DESC",
            PageIndex = 1,
            PageSize = 20
        };


        private Model _model = new Model();


        private async void OnFinish(EditContext editContext)
        {
            _request.PageIndex = 1;
            await CollectData();
            //await GetData();
            StateHasChanged();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }


        private async Task CollectData()
        {
            _request.StartDate = _model.RangePicker != null ? _model.RangePicker[0].Value : DefaultStartDate;
            _request.EndDate = _model.RangePicker != null ? DateTimeExtend.EndOfDay(_model.RangePicker[1].Value) : DefaultEndDate;
            _request.Service = string.IsNullOrWhiteSpace(_model.Service) ? null : _model.Service;
            _request.CustomerCode = string.IsNullOrWhiteSpace(_model.CustomerCode) ? null : _model.CustomerCode;
            _request.PayBillStatus = _model.PayBillStatus == -9999 ? null : _model.PayBillStatus;
            _request.TransactionId = string.IsNullOrWhiteSpace(_model.TransactionId) ? null : _model.TransactionId;
            _request.BillingCode = string.IsNullOrWhiteSpace(_model.BillingCode) ? null : _model.BillingCode;
            _request.SortExpr = _model.SortExpr;
            _request.SortDir = _model.SortDir;
            _request.SendMail = _model.SendMail;
            await Task.CompletedTask;
        }
    }
}
