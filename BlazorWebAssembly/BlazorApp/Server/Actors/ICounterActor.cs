using Dapr.Actors;

namespace BlazorApp.Server.Actors
{
    public interface ICounterActor : IActor
    {
        Task<int> AddCounterAsync();
        Task<int> GetCountAsync();
    }
}
