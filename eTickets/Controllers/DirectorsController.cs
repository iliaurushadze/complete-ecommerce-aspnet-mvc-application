using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly AppDbContext _context;
        public DirectorsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var directors = await _context.Directors.ToListAsync();
            return View(directors);
        }
    }
}
