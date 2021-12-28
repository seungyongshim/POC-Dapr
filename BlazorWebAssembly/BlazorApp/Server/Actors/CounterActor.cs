using Dapr.Actors.Runtime;

namespace BlazorApp.Server.Actors;

public class CounterActor : Actor, ICounterActor
{
    public CounterActor(ActorHost host) : base(host)
    {

    }

    public async Task<int> GetCountAsync()
    {

        var q = from x in StateManager.TryGetStateAsync<CounterActorState>("CounterActorState").ToAff()
                from b in Eff(() => x.Value switch
                {
                    CounterActorState c => c.Count,
                    _ => 0
                })
                select b;

        var r = await q.Retry(Schedule.Recurs(10) | Schedule.Fibonacci(100 * milliseconds)).Run();

        return r.ThrowIfFail();
    }

    public async Task<int> AddCounterAsync()
    {
        var q = from x in StateManager.AddOrUpdateStateAsync(
                    "CounterActorState",
                    new CounterActorState(1),
                    (s, v) => v with { Count = 1 + v.Count }).ToAff()
                from b in Eff(() => x.Count)
                select b;

        var r = await q.Retry(Schedule.Recurs(10) | Schedule.Fibonacci(100 * milliseconds)).Run();
        return r.ThrowIfFail();
    }
}

public record CounterActorState(int Count);
