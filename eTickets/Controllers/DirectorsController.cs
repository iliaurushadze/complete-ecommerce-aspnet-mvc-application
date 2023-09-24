using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _service;
        public DirectorsController(IDirectorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var directors = await _service.GetAllAsync();
            return View(directors);
        }

        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Director director)
        {
            if (director.Id == null || director.ProfilePictureURL == null || director.FullName == null || director.Bio == null)
            {
                return View(director);
            }
            await _service.AddAsync(director);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var directorDetails = await _service.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");
            return View(directorDetails);
        }

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0)
            {
                return View("NotFound");
            }
            var director = await _service.GetByIdAsync(id);
            if (director == null)
            {
                return View("NotFound");
            }
            return View(director);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Director director)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(director);
            //}
            if (director.Id == null || director.ProfilePictureURL == null || director.FullName == null || director.Bio == null)
            {
                return View(director);
            }
            await _service.UpdateAsync(id, director);
            return RedirectToAction(nameof(Index));
        }

        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var director = await _service.GetByIdAsync(id);
            if (director == null) return View("NotFound");
            return View(director);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var director = await _service.GetByIdAsync(id);
            if (director == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
