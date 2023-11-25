using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    partial class FetchData
    {
        [Inject]
        public WeatherForecastService ForecastService { get; set; }

        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
//#if DEBUG
//            await Task.Delay(10000);
//#endif
            forecasts = await ForecastService.GetForecastAsync(DateOnly.FromDateTime(DateTime.Now));
        }
    }
}
