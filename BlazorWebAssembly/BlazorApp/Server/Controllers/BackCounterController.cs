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
            var ret = await CounterActor.GetCountAsync();
            return ret;
        }

        [HttpGet("Add")]
        public async Task<int> Add()
        {
            var ret = await CounterActor.AddCounterAsync();
            return ret;
        }
    }
}
