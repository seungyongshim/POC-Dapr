using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Shared;

public record ActorState<T>
(
    string Id,
    DateTime InsertDate,
    DateTime UpdateDate,
    [property: Column(TypeName = "json")]
    T Value
);


