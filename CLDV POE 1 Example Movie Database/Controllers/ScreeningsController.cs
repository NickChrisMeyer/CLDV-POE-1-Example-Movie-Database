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

            if (await HasOverlapAsync(screening))
            {
                ModelState.AddModelError("", "This cinema already has a screening in the selected time range.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(screening.MovieId, screening.CinemaId);
                return View(screening);
            }

            _db.Screenings.Add(screening);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var screening = await _db.Screenings
                .Include(s => s.Movie)
                .Include(s => s.Cinema)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ScreeningId == id);

            return screening is null ? NotFound() : View(screening);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var screening = await _db.Screenings.FindAsync(id);
            if (screening is null) return NotFound();

            await PopulateDropdowns(screening.MovieId, screening.CinemaId);
            return View(screening);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Screening screening)
        {
            if (id != screening.ScreeningId) return NotFound();

            if (screening.StartTime >= screening.EndTime)
            {
                ModelState.AddModelError("", "Start time must be before end time.");
            }

            if (await HasOverlapAsync(screening))
            {
                ModelState.AddModelError("", "This cinema already has a screening in the selected time range.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(screening.MovieId, screening.CinemaId);
                return View(screening);
            }

            _db.Update(screening);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var screening = await _db.Screenings
                .Include(s => s.Movie)
                .Include(s => s.Cinema)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ScreeningId == id);

            return screening is null ? NotFound() : View(screening);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var screening = await _db.Screenings.FindAsync(id);
            if (screening is not null)
            {
                _db.Screenings.Remove(screening);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HasOverlapAsync(Screening screening)
        {
            return await _db.Screenings.AnyAsync(s =>
                s.CinemaId == screening.CinemaId &&
                s.ScreeningId != screening.ScreeningId &&
                screening.StartTime < s.EndTime &&
                screening.EndTime > s.StartTime);
        }

        private async Task PopulateDropdowns(int? selectedMovieId = null, int? selectedCinemaId = null)
        {
            ViewBag.MovieId = new SelectList(
                await _db.Movies.AsNoTracking().OrderBy(m => m.Title).ToListAsync(),
                "MovieId", "Title", selectedMovieId);

            ViewBag.CinemaId = new SelectList(
                await _db.Cinemas.AsNoTracking().OrderBy(c => c.Name).ToListAsync(),
                "CinemaId", "Name", selectedCinemaId);
        }
    }
}
