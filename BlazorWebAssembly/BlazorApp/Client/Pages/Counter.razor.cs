using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public partial class Counter
    {
        [Inject]
        HttpClient HttpClient { get; set; }

        private string currentCount;
        private async Task IncrementCount()
        {
            currentCount = await HttpClient.GetStringAsync("Backend/Counter/Add");
        }

        protected override async Task OnInitializedAsync() 
        {
            currentCount = await HttpClient.GetStringAsync("Backend/Counter");
            StateHasChanged();
        }
    }
}
