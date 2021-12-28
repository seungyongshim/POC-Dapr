using BlazorApp.Client.Pages;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Client
{
    public partial class App : IAsyncDisposable
    {
        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public HubConnection Connection { get; set; }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) 
        {
            Connection.On<int>("Counter", x =>
            {
                Mediator.Send(new CounterRequest(x));
            });

            await Connection.StartAsync();
        }

    }
}
