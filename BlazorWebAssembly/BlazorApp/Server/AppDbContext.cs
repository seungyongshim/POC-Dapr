using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorApp.Server;

public class AppDbContext : DbContext
{ 
    public virtual IQueryable<ActorState<T>> QueryActorState<T>()
        where T : class =>
            Set<ActorState<T>>().FromSqlRaw($"select * from state where MATCH(id) AGAINST('{typeof(T)}' IN BOOLEAN MODE)"
                );

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActorState<CounterActorState>>().HasNoKey().ToView(null);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyUtcDateTimeConverter();
    }
}


//https://github.com/dotnet/efcore/issues/4711#issuecomment-734471122

public static class UtcDateAnnotation
{
    private const string IsUtcAnnotation = "IsUtc";
    private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
        new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, bool isUtc = true) =>
        builder.HasAnnotation(IsUtcAnnotation, isUtc);

    public static bool IsUtc(this IMutableProperty property)
    {
        var attribute = property.PropertyInfo.GetCustomAttribute<IsUtcAttribute>();
        if (attribute is not null && attribute.IsUtc)
        {
            return true;
        }

        return ((bool?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;
    }

    /// <summary>
    /// Make sure this is called after configuring all your entities.
    /// </summary>
    public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (!property.IsUtc())
                {
                    continue;
                }

                if (property.ClrType == typeof(DateTime) ||
                    property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(UtcConverter);
                }
            }
        }
    }
}

