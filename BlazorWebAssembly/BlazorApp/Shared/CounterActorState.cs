using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared;

public record CounterActorState([property: JsonPropertyName("count")] int Count);

public record ActorState<T>
(
    string Id,
    [property: Column(TypeName = "json")]
    T Value,
    [property: IsUtc]
    DateTime InsertDate,
    [property: IsUtc]
    DateTime UpdateDate
);


public class IsUtcAttribute : Attribute
{
    public IsUtcAttribute(bool isUtc = true) => IsUtc = isUtc;
    public bool IsUtc { get; }
}


