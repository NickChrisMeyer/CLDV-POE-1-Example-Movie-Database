namespace CLDV_POE_1_Example_Movie_Database.Data
{
    using CLDV_POE_1_Example_Movie_Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class MovieVaultDbContext : DbContext
    {
        public MovieVaultDbContext(DbContextOptions<MovieVaultDbContext> options) : base(options) { }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Screening> Screenings => Set<Screening>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Guardrail: time range must be valid
            modelBuilder.Entity<Screening>()
                .ToTable(t => t.HasCheckConstraint("CK_Screening_TimeRange", "[StartTime] < [EndTime]"));

            // Guardrail: prevent cascade delete surprises for beginners
            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Screenings)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Cinema)
                .WithMany(c => c.Screenings)
                .HasForeignKey(s => s.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
