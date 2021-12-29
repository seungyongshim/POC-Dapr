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
            currentCount = await HttpClient.GetStringAsync("b/Counter/Add");
        }

        protected override async Task OnInitializedAsync() 
        {
            currentCount = await HttpClient.GetStringAsync("b/Counter");
            StateHasChanged();
        }
    }
}
