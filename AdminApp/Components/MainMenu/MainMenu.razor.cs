using AdminApp.Services;
using AntDesign;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using ReportService;
using System.Security.Claims;

namespace AdminApp.Components
{
    public partial class MainMenu
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        AuthenticationState authState;
        ClaimsPrincipal currentUser;
        //Metadata _header = new Metadata();
        [Parameter] public MenuTheme ThemeStyle { get; set; } = MenuTheme.Dark;
        [Inject] protected IAppStateService _appStateService { get; set; }
        [Inject] protected MessageService _antMessage { get; set; }
        [Inject] private Transaction.TransactionClient _transaction { get; set; }
        [Inject] private RequestSupport.RequestSupportClient _requestSupportClient { get; set; }
        [Inject] protected IMapper _mapper { get; set; }
        [Inject] protected ILogger<MainMenu> _logger { get; set; }
        [Inject] private IMemoryCache _memoryCache { get; set; }
        [Inject] private IConfiguration _configuration { get; set; }
        [Inject] private IHttpContextAccessor _httpContextAccessor { get; set; }

        private int? NumberTransactionPending = null;
        private int? NumberRequestSupportPending = null;
        private int Expried_CountTransactionPending = 30;
        private async Task GetCountTransactionPendingAsync()
        {
            try
            {


                int? countTransactionPending = _memoryCache.Get<int?>("CountTransactionPending");
                if (countTransactionPending == null)
                {
                    var result = await _transaction.GetCountTransactionPendingAsync(new GetCountTransactionPendingRequest());
                    NumberTransactionPending = result.CountData;
                    _memoryCache.Set("CountTransactionPending", result.CountData, TimeSpan.FromSeconds(Expried_CountTransactionPending));
                }
                else
                {
                    NumberTransactionPending = countTransactionPending;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }

        }

        private async Task GetCountRequestSupportPendingAsync()
        {
            try
            {

                int? countTransactionPending = _memoryCache.Get<int?>("CountRequestSupportPending");
                if (countTransactionPending == null)
                {
                    var result = await _requestSupportClient.GetCountRequestSupportPendingAsync(new GetCountRequestSupportPendingRequest());
                    NumberRequestSupportPending = result.CountData;
                    _memoryCache.Set("CountRequestSupportPending", result.CountData, TimeSpan.FromSeconds(Expried_CountTransactionPending));
                }
                else
                {
                    NumberRequestSupportPending = countTransactionPending;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }
        protected override async Task OnInitializedAsync()
        {
            authState = await authenticationStateTask;
            currentUser = authState.User;
            Expried_CountTransactionPending = _configuration.GetValue<int>("Expried_CountTransactionPending");
            //var token = _httpContextAccessor.HttpContext?.Request.Cookies["access_token"];
            //_header.Add("Authorization", $"Bearer {token}");
            await base.OnInitializedAsync();

        }

        protected override void OnAfterRender(bool firstRender)
        {

            if (firstRender)
            {
                var autoEvent = new AutoResetEvent(false);
                var stateTimer = new System.Threading.Timer(new TimerCallback(TimerTask), autoEvent, 0, Expried_CountTransactionPending * 1000);
            }
            base.OnAfterRender(firstRender);
        }

        private async void TimerTask(object timerState)
        {
            await GetCountTransactionPendingAsync();
            await GetCountRequestSupportPendingAsync();
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
    }
}
