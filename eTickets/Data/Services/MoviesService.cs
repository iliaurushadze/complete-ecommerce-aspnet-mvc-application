using eTickets.Data.Base;
using eTickets.Models;
using eTickets.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(d => d.Director)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Directors = await _context.Directors.OrderBy(d => d.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync()
            };

            return response;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                DirectorId = data.DirectorId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add movie actors
            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actor_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(NewMovieVM newMovieVM)
        {
            var dbMovie = await _context.Movies.SingleOrDefaultAsync(n => n.Id == newMovieVM.Id);
            if(dbMovie.Id != null)
            {
                dbMovie.Name = newMovieVM.Name;
                dbMovie.Description = newMovieVM.Description;
                dbMovie.Price = newMovieVM.Price;
                dbMovie.ImageURL = newMovieVM.ImageURL;
                dbMovie.CinemaId = newMovieVM.CinemaId;
                dbMovie.StartDate = newMovieVM.StartDate;
                dbMovie.EndDate = newMovieVM.EndDate;
                dbMovie.MovieCategory = newMovieVM.MovieCategory;
                dbMovie.DirectorId = newMovieVM.DirectorId;
                await _context.SaveChangesAsync();
            }

            // Remove Exiting actors
            var exitingActorsDb = _context.Actor_Movies.Where(n => n.MovieId == newMovieVM.Id).ToList();
            _context.Actor_Movies.RemoveRange(exitingActorsDb);
            await _context.SaveChangesAsync();
            

            //Add movie actors
            foreach (var actorId in newMovieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = newMovieVM.Id,
                    ActorId = actorId
                };
                await _context.Actor_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
