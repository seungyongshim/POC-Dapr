global using LanguageExt;
global using static LanguageExt.Prelude;
using Microsoft.AspNetCore.ResponseCompression;
using Dapr.Actors.Client;
using BlazorApp.Server.Actors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<CounterActor>();
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapActorsHandlers();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
