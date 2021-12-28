namespace BlazorApp.Server;

public record CounterActorState(int Count);
public class CounterActorStatePoco
{
    public int count { get; set; }
}
