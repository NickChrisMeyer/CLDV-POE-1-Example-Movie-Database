namespace CLDV_POE_1_Example_Movie_Database.Controllers
{
    using CLDV_POE_1_Example_Movie_Database.Data;
    using CLDV_POE_1_Example_Movie_Database.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class MoviesController : Controller
    {
        private readonly MovieVaultDbContext _db;
        public MoviesController(MovieVaultDbContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Movies.AsNoTracking().OrderBy(m => m.Title).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var movie = await _db.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.MovieId == id);
            return movie is null ? NotFound() : View(movie);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);

            _db.Movies.Add(movie);
            await _db.SaveChangesAsync();
            //Display confirm message
            Thread.Sleep(5000);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var movie = await _db.Movies.FindAsync(id);
            return movie is null ? NotFound() : View(movie);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.MovieId) return NotFound();
            if (!ModelState.IsValid) return View(movie);

            _db.Update(movie);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var movie = await _db.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.MovieId == id);
            return movie is null ? NotFound() : View(movie);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie is not null)
            {
                var hasScreenings = await _db.Screenings.AnyAsync(s => s.MovieId == id);
                if (hasScreenings)
                {
                    ModelState.AddModelError(string.Empty, "Cannot delete this movie because it has scheduled screenings.");
                    return View(movie);
                }

                _db.Movies.Remove(movie);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
