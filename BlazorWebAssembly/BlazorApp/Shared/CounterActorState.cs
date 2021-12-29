using System.Text.Json.Serialization;

namespace BlazorApp.Shared;

public record CounterActorState([property: JsonPropertyName("count")] int Count);


