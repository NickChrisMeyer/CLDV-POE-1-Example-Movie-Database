namespace CLDV_POE_1_Example_Movie_Database.Controllers
{
    using CLDV_POE_1_Example_Movie_Database.Data;
    using CLDV_POE_1_Example_Movie_Database.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class ScreeningsController : Controller
    {
        private readonly MovieVaultDbContext _db;
        public ScreeningsController(MovieVaultDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var screenings = await _db.Screenings
                .Include(s => s.Movie)
                .Include(s => s.Cinema)
                .AsNoTracking()
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();

            return View(screenings);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Screening screening)
        {
            if (screening.StartTime >= screening.EndTime)
            {
                ModelState.AddModelError("", "Start time must be before end time.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(screening);
            }

            _db.Screenings.Add(screening);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.MovieId = new SelectList(
                await _db.Movies.AsNoTracking().OrderBy(m => m.Title).ToListAsync(),
                "MovieId", "Title");

            ViewBag.CinemaId = new SelectList(
                await _db.Cinemas.AsNoTracking().OrderBy(c => c.Name).ToListAsync(),
                "CinemaId", "Name");
        }
    }
}
