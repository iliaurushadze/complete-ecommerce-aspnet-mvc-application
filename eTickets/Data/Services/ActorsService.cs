using eTickets.Models;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context)
        {

            _context = context;

        }
        public void AddActor(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }

        public void DeleteActor(int id)
        {
            throw new NotImplementedException();
        }

        public Actor GetActorById(int id)
        {
            return _context.Actors.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Actor> GetActors()
        {
            var result = _context.Actors.ToList();
            return result;
        }

        public Actor UpdateActor(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
