using eTickets.Data;
using eTickets.Data.Enums;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync(n => n.Cinema);
            return View(movies);
        }
        //Get Details
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        // Get Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Directors = new SelectList(movieDropdownData.Directors, "Id", "FullName");
            return View();
        }

        //Post Create
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovieVM)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                ViewBag.Directors = new SelectList(movieDropdownData.Directors, "Id", "FullName");
                return View(newMovieVM);
            }
            await _service.AddNewMovieAsync(newMovieVM);
            return RedirectToAction(nameof(Index));
        }

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return View("NotFound");

            var data = await _service.GetMovieByIdAsync(id);
            if (data == null) return View("NotFound");

            var movie = new NewMovieVM
            {
                Id = id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                DirectorId = data.DirectorId,
                ActorIds = data.Actor_Movies.Select(a => a.ActorId).ToList()
            };

            var movieDropdownData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Directors = new SelectList(movieDropdownData.Directors, "Id", "FullName");
            return View(movie);
        }

        //Post Update
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM newMovieVM)
        {
            if (id != newMovieVM.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                ViewBag.Directors = new SelectList(movieDropdownData.Directors, "Id", "FullName");

            }
            await _service.UpdateMovieAsync(newMovieVM);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var movies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = movies.Where(m => m.Name.Contains(searchString) || 
                m.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", movies);
        }
    }
}
