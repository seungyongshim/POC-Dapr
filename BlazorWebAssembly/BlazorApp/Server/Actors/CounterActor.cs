using Dapr.Actors.Runtime;

namespace BlazorApp.Server.Actors
{

    public class CounterActor : Actor, ICounterActor
    {
        public CounterActor(ActorHost host) : base(host)
        {

        }

        public Task<int> GetCountAsync() =>
            StateManager.GetStateAsync<int>("Counter");

        public Task<int> AddCounterAsync() =>
            StateManager.AddOrUpdateStateAsync<int>("Counter", 1, (s, v) => 1 + v);
    }
}
