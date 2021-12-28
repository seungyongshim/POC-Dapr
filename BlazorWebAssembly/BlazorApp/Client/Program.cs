using BlazorApp.Client;
using BlazorApp.Client.Pages;
using BlazorApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMediatR(typeof(CounterRequest));

builder.Services.AddSingleton<HubConnection>(sp => new HubConnectionBuilder()
    .WithUrl(sp.GetService<NavigationManager>().BaseUri + "Hub")
    .AddJsonProtocol()
    .WithAutomaticReconnect()
    .Build());

await builder.Build().RunAsync();
