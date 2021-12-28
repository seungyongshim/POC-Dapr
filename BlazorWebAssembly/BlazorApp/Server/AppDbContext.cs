using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server;

public class AppDbContext : DbContext
{
    public virtual DbSet<ActorStates> CounterActorStates { get; set; }

    public virtual IQueryable<ActorStates> QueryCounterActorStates =>
        CounterActorStates.FromSqlRaw(
            "select * from state " +
            "where MATCH(id) AGAINST('CounterActorState' IN BOOLEAN MODE)"
            );

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}         

public record ActorStates
(
    string Id,
    [property: Column(TypeName = "json")]
    CounterActorStatePoco Value
);
