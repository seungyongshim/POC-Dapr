using System.Net.Http.Json;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public partial class FetchData
    {
        [Inject]
        HttpClient Http { get; set; }

        private ActorState<CounterActorState>[]? forecasts;
        protected override async Task OnInitializedAsync()
        {
            forecasts = await Http.GetFromJsonAsync<ActorState<CounterActorState>[]>("b/FetchData");
        }
    }
}
