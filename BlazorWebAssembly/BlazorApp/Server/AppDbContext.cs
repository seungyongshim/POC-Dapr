using System.Reflection;
using BlazorApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server;

public class AppDbContext : DbContext
{
    public virtual IQueryable<ActorState<T>> QueryActorState<T>()
        where T : class =>
            Set<ActorState<T>>().FromSqlRaw($"select * from state where MATCH(id) AGAINST('{typeof(T).Name}' IN BOOLEAN MODE)"
                );

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActorState<CounterActorState>>().HasNoKey().ToView(null);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

//https://github.com/dotnet/efcore/issues/4711#issuecomment-734471122
