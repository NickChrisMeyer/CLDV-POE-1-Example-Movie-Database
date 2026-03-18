namespace CLDV_POE_1_Example_Movie_Database.Data
{
    using CLDV_POE_1_Example_Movie_Database.Models;
    using Microsoft.EntityFrameworkCore;

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MovieVaultDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MovieVaultDbContext>>());

            context.Database.Migrate();

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                    new Movie
                    {
                        Title = "Interstellar",
                        Genre = "Sci-Fi",
                        ReleaseDate = new DateTime(2014, 11, 7),
                        PosterUrl = "https://picsum.photos/seed/interstellar/600/900"
                    },
                    new Movie
                    {
                        Title = "The Dark Knight",
                        Genre = "Action",
                        ReleaseDate = new DateTime(2008, 7, 18),
                        PosterUrl = "https://picsum.photos/seed/darkknight/600/900"
                    },
                    new Movie
                    {
                        Title = "Inception",
                        Genre = "Thriller",
                        ReleaseDate = new DateTime(2010, 7, 16),
                        PosterUrl = "https://picsum.photos/seed/inception/600/900"
                    });
            }

            if (!context.Cinemas.Any())
            {
                context.Cinemas.AddRange(
                    new Cinema
                    {
                        Name = "Nova Cinema Sandton",
                        City = "Johannesburg",
                        Seats = 180,
                        ImageUrl = "https://picsum.photos/seed/sandton/800/450"
                    },
                    new Cinema
                    {
                        Name = "Cape Star IMAX",
                        City = "Cape Town",
                        Seats = 240,
                        ImageUrl = "https://picsum.photos/seed/capetown/800/450"
                    });
            }

            context.SaveChanges();

            if (!context.Screenings.Any())
            {
                var movies = context.Movies.AsNoTracking().OrderBy(m => m.MovieId).ToList();
                var cinemas = context.Cinemas.AsNoTracking().OrderBy(c => c.CinemaId).ToList();

                if (movies.Count >= 3 && cinemas.Count >= 2)
                {
                    var today = DateTime.Today;
                    context.Screenings.AddRange(
                        new Screening
                        {
                            MovieId = movies[0].MovieId,
                            CinemaId = cinemas[0].CinemaId,
                            StartTime = today.AddHours(15),
                            EndTime = today.AddHours(17)
                        },
                        new Screening
                        {
                            MovieId = movies[1].MovieId,
                            CinemaId = cinemas[0].CinemaId,
                            StartTime = today.AddHours(18),
                            EndTime = today.AddHours(20)
                        },
                        new Screening
                        {
                            MovieId = movies[2].MovieId,
                            CinemaId = cinemas[1].CinemaId,
                            StartTime = today.AddHours(16),
                            EndTime = today.AddHours(18)
                        });
                }
            }

            context.SaveChanges();
        }
    }
}
