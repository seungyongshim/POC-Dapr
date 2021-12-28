using Microsoft.AspNetCore.SignalR;

public class ServerHub : Hub
{
    public Task CountSendAsync(int count) => Clients.All.SendAsync("Counter", count);
}
