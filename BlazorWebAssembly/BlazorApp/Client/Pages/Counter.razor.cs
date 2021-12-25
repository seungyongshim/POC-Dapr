using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public partial class Counter
    {
        [Inject]
        HttpClient HttpClient { get; set; }

        private int? currentCount;
        private async Task IncrementCount()
        {
            var ret = await HttpClient.GetStringAsync("BackCounter/Add");
            currentCount = Convert.ToInt32(ret);
        }

        protected override async Task OnInitializedAsync()
        {
            var ret = await HttpClient.GetStringAsync("BackCounter");
            currentCount = Convert.ToInt32(ret);
        }
    }
}
