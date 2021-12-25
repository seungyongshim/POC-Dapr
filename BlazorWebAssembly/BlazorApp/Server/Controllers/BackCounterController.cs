using Microsoft.AspNetCore.Mvc;
using Dapr.Actors.Client;
using BlazorApp.Server.Actors;
using Dapr.Actors;

namespace BlazorApp.Server.Controllers
{
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
            var q = CounterActor.GetCountAsync().ToAff().Retry(Schedule.Recurs(10) | Schedule.Fibonacci(100 * milliseconds));
                    
            var ret = await q.Run();
            return ret.ThrowIfFail();
        }

        [HttpGet("Add")]
        public async Task<int> Add()
        {
            var ret = await CounterActor.AddCounterAsync();
            return ret;
        }
    }
}
