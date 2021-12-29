using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers;

[ApiController]
[Route("/b/[controller]")]
public class ManageUsersController : Controller
{

    [HttpGet]
    public IList<ActorState<UserActorState>> Get()
    {
        return new List<ActorState<UserActorState>>
        {
            new ActorState<UserActorState>("1", DateTime.UtcNow, DateTime.UtcNow,
                new UserActorState(
                    new Company("Nexon", "넥슨코리아"),
                    new Department("TF", "티에프"),
                    new BackOfficeUser("홍사모", new Email("xxx@nexon.com")),
                    "시험사용",
                    DateTime.UtcNow + (360 * days)
                    )),
            new ActorState<UserActorState>("2", DateTime.UtcNow, DateTime.UtcNow,
                new UserActorState(
                    new Company("Nexon", "넥슨코리아"),
                    new Department("AF", "심심"),
                    new BackOfficeUser("홍길동", new Email("bbb@nexon.com")),
                    "시험사용",
                    DateTime.UtcNow + (360 * days)
                    )),
        };
    }
}
