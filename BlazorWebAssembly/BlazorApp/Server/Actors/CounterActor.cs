using BlazorApp.Shared;
using Dapr.Actors.Runtime;
using static BlazorApp.Server.Actors.AffHelper;

namespace BlazorApp.Server.Actors;

public static partial class AffHelper
{
    public static Aff<T> getStateAff<T>(IActorStateManager actorStateManager) =>
        from a in Eff(() => $"{typeof(T).Name}")
        from x in actorStateManager.TryGetStateAsync<T>(a).ToAff()
        from b in Eff(() => x.Value switch
        {
            T c => c,
            _ => default
        })
        select b;
}

public class CounterActor : Actor, ICounterActor
{
    public CounterActor(ActorHost host) : base(host)
    {

    }

    public async Task<int> GetCountAsync()
    {
        var q = from x in getStateAff<CounterActorState>(StateManager)
                from b in Eff(() => x.Count)
                select b;

        var r = await q.Retry(Schedule.Recurs(3) | Schedule.Fibonacci(100 * milliseconds)).Run();

        return r.IfFail(e => 0);
    }

    public async Task<int> AddCounterAsync()
    {
        var q = from x in StateManager.AddOrUpdateStateAsync(
                    "CounterActorState",
                    new CounterActorState(1),
                    (s, v) => v with { Count = 1 + v.Count }).ToAff()
                from b in Eff(() => x.Count)
                select b;

        var r = await q.Retry(Schedule.Recurs(3) | Schedule.Fibonacci(100 * milliseconds)).Run();
        return r.ThrowIfFail();
    }
}
