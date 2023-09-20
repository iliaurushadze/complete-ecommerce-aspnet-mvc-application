using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
        IEnumerable<Actor> GetActors();
        Actor GetActorById(int id);
        void AddActor(Actor actor);
        Actor UpdateActor(int id, Actor newActor);
        void DeleteActor(int id);
    }
}
