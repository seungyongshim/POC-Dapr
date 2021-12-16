using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Counter2Controller : Controller
    {
        [HttpGet]
        public int Get([FromState("statestore", "count")] StateEntry<int> counter)
        {
            return counter.Value;
        }
    }
}
