using Microsoft.AspNetCore.Mvc;
using Dapr.Actors.Client;
using BlazorApp.Server.Actors;
using Dapr.Actors;

namespace BlazorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class BackCounterController : Controller
{
    public BackCounterController(IActorProxyFactory actorProxy)
    {
        CounterActor = actorProxy.CreateActorProxy<ICounterActor>(new ActorId("1"), "CounterActor");
    }

    public ICounterActor CounterActor { get; }

    [HttpGet]
    public async Task<int> Get()
    {
        var q = from x in CounterActor.GetCountAsync().ToAff()
                select x;

        var r = await q.Retry(Schedule.Recurs(10) | Schedule.Fibonacci(100 * milliseconds)).Run();
        return r.ThrowIfFail();
    }

    [HttpGet("Add")]
    public async Task<int> Add()
    {
        var q = from x in CounterActor.AddCounterAsync().ToAff()
                select x;

        var r = await q.Retry(Schedule.Recurs(10) | Schedule.Fibonacci(100 * milliseconds)).Run();
        return r.ThrowIfFail();
    }
}
