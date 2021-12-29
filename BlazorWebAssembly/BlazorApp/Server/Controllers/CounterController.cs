using Microsoft.AspNetCore.Mvc;
using Dapr.Actors.Client;
using BlazorApp.Server.Actors;
using Dapr.Actors;

namespace BlazorApp.Server.Controllers;

[ApiController]
[Route("/Backend/[controller]")]
public class CounterController : Controller
{
    public CounterController(IActorProxyFactory actorProxy)
    {
        CounterActor = actorProxy.CreateActorProxy<ICounterActor>(new ActorId("2"), "CounterActor");
    }

    public ICounterActor CounterActor { get; }

    [HttpGet]
    public async Task<int> Get()
    {
        var q = from x in CounterActor.GetCountAsync().ToAff()
                select x;

        var r = await q.Retry(Schedule.Recurs(3) | Schedule.Fibonacci(100 * milliseconds)).Run();
        return r.ThrowIfFail();
    }

    [HttpGet("Add")]
    public async Task<int> Add()
    {
        var q = from x in CounterActor.AddCounterAsync().ToAff()
                select x;

        var r = await q.Retry(Schedule.Recurs(3) | Schedule.Fibonacci(100 * milliseconds)).Run();
        return r.ThrowIfFail();
    }
}
