using Dapr.Client;
using Microsoft.AspNetCore.Components;

namespace BlazorFront.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;
        const string storeName = "statestore";
        const string key = "counter";

        [Inject]
        public DaprClient DaprClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            currentCount = await DaprClient.GetStateAsync<int>(storeName, key);
            await base.OnInitializedAsync();
        }

        private async Task IncrementCountAsync()
        {
            currentCount++;
            await DaprClient.SaveStateAsync(storeName, key, currentCount);
        }

       
    }
}
