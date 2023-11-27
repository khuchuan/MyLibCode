using AntDesign;
using BlazorApp1.Models;
using Helper;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using DTO;
using Microsoft.Extensions.Logging;


namespace BlazorApp1.Pages
{
    public partial class TransactionPage
    {
        [Inject] private Blazored.LocalStorage.ILocalStorageService _localStorageService { get; set; }

        private static DateTime DefaultStartDate = DateTime.Today.AddDays(-7);
        private static DateTime DefaultEndDate = DateTime.Today;

        private List<DTO.TransactionHistoryInfo> _dataTransactionDTO;
        private string _transactionId;
        bool visibleModalViewDetail = false;
        private IEnumerable<string> _selectedColumns { get; set; } = new List<string>();


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

        private List<Column> _columns = new List<Column>() {
            new Column { Key = "CreatedTime", Title = "Thời gian tạo" },
            new Column { Key = "ResponseTime", Title = "Thời gian phản hồi" },
            new Column { Key = "TrackingId", Title = "Mã giao dịch" },
            new Column { Key = "Username", Title = "Khách hàng" },
            new Column { Key = "Partner", Title = "Đối tác" },
            new Column { Key = "ServiceId", Title = "Dịch vụ" },
            new Column { Key = "ProductCode", Title = "Sản phẩm" },
            new Column { Key = "PackCode", Title = "Mã gói" },
            new Column { Key = "BillingCode", Title = "Mã hóa đơn" },
            new Column { Key = "Amount", Title = "Mệnh giá" },
            new Column { Key = "ActualAmount", Title = "Số tiền" },
            new Column { Key = "Status", Title = "Trạng thái" },
            new Column { Key = "SendMail", Title = "Trạng thái Mail" },
            new Column { Key = "ResultCode", Title = "Mã kết quả" }
        };

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
            await GetData();
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

        // Fake du lieu
        private async Task GetData()
        {
            _dataTransactionDTO = new List<DTO.TransactionHistoryInfo>();

            int numberData = 60;
            for (int i = 0; i < numberData; i++)
            {
                TransactionHistoryInfo tranInfo = new TransactionHistoryInfo()
                {
                    ActualAmount = 20000 + i * 1000,
                    Amount = 20000 + i * 1000,
                    BillingCode = "090123" + i.ToString().PadLeft(5, '0'),
                    CreatedTime = DateTime.Now,
                    CustomerId = "iristest",
                    Id = DateTime.Now.ToString("HHmmss"),
                    PackCode = "STK00X",
                    Partner = "VnPay",
                    ProductCode = "MOBIFONE_DATA",
                    ProductName = "Goi da ta menh gia " + (i * 1000).ToString() + " VND",
                    Quantity = 1,
                    ResponseTime = DateTime.Now,
                    ResultCode = "00",
                    Status = 9,
                    TrackingId =  i.ToString().PadLeft(4, '0'),
                    ServiceId = "DATA",
                    Username = "usernametest",
                    TotalRows = numberData,
                    SendMail = 0
                };
                _dataTransactionDTO.Add(tranInfo);
            }
        }

        private async Task OnShowSizeChange(PaginationEventArgs args)
        {
            _request.PageIndex = args.Page;
            _request.PageSize = args.PageSize;
            await GetData();
        }

        private async void HandleClickSort(string sortExpr)
        {
            if (sortExpr == _model.SortExpr)
            {
                if (_model.SortDir == "DESC")
                {
                    _model.SortDir = "ASC";
                }
                else
                {
                    _model.SortExpr = "CreateDate";
                    _model.SortDir = "DESC";
                }
            }
            else
            {
                _model.SortExpr = sortExpr;
                _model.SortDir = "DESC";
            }
            await CollectData();
            await GetData();
        }

        private string RenderClassSortIcon(string itemSortExpr)
        {
            return _model.SortExpr == itemSortExpr ? "rotate-sort-icon-active" : "rotate-sort-icon";
        }

        private string RenderTypeSortIcon()
        {
            return _model.SortDir == "DESC" ? "sort-ascending" : "sort-descending";
        }

        private async Task HandleSendMailSoftpin(DTO.TransactionHistoryInfo context)
        {
            try
            {
            }
            catch (Exception ex)
            {         
            }
        }


        private void HandleClickViewDetail(string id)
        {
            _transactionId = id;
            visibleModalViewDetail = true;
            StateHasChanged();
        }





    }
}
