using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetActorsAsync();
            return View(actors);
        }

        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddActorAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetActorByIdAsync(id);
            if (actorDetails == null) /*return NotFound();*/ return View("NotFound");
            return View(actorDetails);
        }

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var actor = await _service.GetActorByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateActorAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetActorByIdAsync(id);
            if (actor == null) return NotFound();
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var actor = await _service.GetActorByIdAsync(id);
            if (actor == null) return NotFound();

            await _service.DeleteActorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
