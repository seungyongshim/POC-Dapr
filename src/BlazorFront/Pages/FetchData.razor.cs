using BlazorFront.Data;
using Dapr.Client;
using Microsoft.AspNetCore.Components;

namespace BlazorFront.Pages
{
    public partial class FetchData
    {
        [Inject]
        public DaprClient DaprClient { get; set; }

        private WeatherForecast[]? forecasts;

        protected override Task OnInitializedAsync() =>
            RefreshAsync();


        private async Task RefreshAsync() =>
            forecasts = await DaprClient.InvokeMethodAsync<WeatherForecast[]>(HttpMethod.Get, "Back", "WeatherForecast");
    }
}
