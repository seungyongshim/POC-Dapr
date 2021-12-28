using BlazorApp.Server.Actors;
using BlazorApp.Shared;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackCountersController : ControllerBase
    {
        public BackCountersController(IActorProxyFactory actorProxy,
                                      AppDbContext dbContext)
        {
            CounterActor = actorProxy.CreateActorProxy<ICounterActor>(new ActorId("1"), "CounterActor");
            DbContext = dbContext;
        }

        public ICounterActor CounterActor { get; private set; }
        public AppDbContext DbContext { get; }

        [HttpGet]
        public IEnumerable<ActorStates> Get()
        {
            var q = from x in DbContext.QueryCounterActorStates
                    select x;

            return q.ToList();
        }

    }
}
