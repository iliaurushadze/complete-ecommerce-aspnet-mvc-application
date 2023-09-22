using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
        Task<IEnumerable<Actor>> GetActorsAsync();
        Task<Actor> GetActorByIdAsync(int id);
        Task AddActorAsync(Actor actor);
        Task<Actor> UpdateActorAsync(int id, Actor newActor);
        Task DeleteActorAsync(int id);
    }
}
