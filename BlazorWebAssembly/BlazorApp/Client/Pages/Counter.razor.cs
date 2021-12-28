using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public partial class Counter : IRequestHandler<CounterRequest>
    {
        [Inject]
        HttpClient HttpClient { get; set; }

        private int? currentCount;
        private async Task IncrementCount() => await HttpClient.GetStringAsync("BackCounter/Add");

        protected override async Task OnInitializedAsync() => await HttpClient.GetStringAsync("BackCounter");

        public Task<Unit> Handle(CounterRequest request, CancellationToken cancellationToken)
        {
            currentCount = request.Count;
            StateHasChanged();
            return Unit.Task;
        }
    }

    public record CounterRequest(int Count) : IRequest;
}
