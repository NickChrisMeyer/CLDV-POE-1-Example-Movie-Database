namespace CLDV_POE_1_Example_Movie_Database.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required, StringLength(40)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(40)]
        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Url, Display(Name = "Poster")]
        public string PosterUrl { get; set; } = "https://picsum.photos/seed/movie/600/900";

        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }
}
