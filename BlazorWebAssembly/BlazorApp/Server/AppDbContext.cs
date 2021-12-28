using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server;

public class AppDbContext : DbContext
{
    public virtual DbSet<ActorStates> CounterActorStates { get;set;}

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


}

[Table("state")]
public record ActorStates
(
    string Id,
    string Value
);
