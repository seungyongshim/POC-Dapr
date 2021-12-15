using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            DaprClient = daprClient;
        }

        public DaprClient DaprClient { get; }

        public async Task OnGet()
        {
            var forecasts = await DaprClient.InvokeMethodAsync<IEnumerable<WeahterForecast>>(
                HttpMethod.Get,
                "Back",
                "WeatherForecast");

            ViewData["WeatherForecastData"] = forecasts;
        }
    }
}
