using Dapr.Actors.Runtime;

namespace BlazorApp.Server.Actors
{

    public class CounterActor : Actor, ICounterActor
    {
        public CounterActor(ActorHost host) : base(host)
        {

        }

        public async Task<int> GetCountAsync() =>
            await StateManager.TryGetStateAsync<int>("Counter") switch
            {
                var ret when ret.HasValue is true => ret.Value,
                _ => 0
            };

        public Task<int> AddCounterAsync() =>
            StateManager.AddOrUpdateStateAsync<int>("Counter", 1, (s, v) => 1 + v);
    }
}
