using BlazorApp.Server.Actors;
using BlazorApp.Shared;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("/Backend/[controller]")]
    public class FetchDataController : ControllerBase
    {
        public FetchDataController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public AppDbContext DbContext { get; }

        [HttpGet()]
        public IList<ActorState<CounterActorState>> Get1()
        {
            var q = from x in DbContext.QueryActorState<CounterActorState>()
                    select x;

            return q.ToList();
        }

    }
}
