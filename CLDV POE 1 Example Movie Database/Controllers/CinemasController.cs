namespace CLDV_POE_1_Example_Movie_Database.Controllers
{
    using CLDV_POE_1_Example_Movie_Database.Data;
    using CLDV_POE_1_Example_Movie_Database.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CinemasController : Controller
    {
        private readonly MovieVaultDbContext _db;
        public CinemasController(MovieVaultDbContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Cinemas.AsNoTracking().OrderBy(c => c.Name).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var cinema = await _db.Cinemas.AsNoTracking().FirstOrDefaultAsync(c => c.CinemaId == id);
            return cinema is null ? NotFound() : View(cinema);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            _db.Cinemas.Add(cinema);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var cinema = await _db.Cinemas.FindAsync(id);
            return cinema is null ? NotFound() : View(cinema);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (id != cinema.CinemaId) return NotFound();
            if (!ModelState.IsValid) return View(cinema);

            _db.Update(cinema);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var cinema = await _db.Cinemas.AsNoTracking().FirstOrDefaultAsync(c => c.CinemaId == id);
            return cinema is null ? NotFound() : View(cinema);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _db.Cinemas.FindAsync(id);
            if (cinema is not null)
            {
                var hasScreenings = await _db.Screenings.AnyAsync(s => s.CinemaId == id);
                if (hasScreenings)
                {
                    ModelState.AddModelError(string.Empty, "Cannot delete this cinema because it has scheduled screenings.");
                    return View(cinema);
                }

                _db.Cinemas.Remove(cinema);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
