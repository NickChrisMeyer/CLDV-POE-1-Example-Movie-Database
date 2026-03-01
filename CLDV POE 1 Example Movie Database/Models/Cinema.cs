namespace CLDV_POE_1_Example_Movie_Database.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cinema
    {
        [Key]
        public int CinemaId { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(60)]
        public string City { get; set; } = string.Empty;

        [Range(10, 1000)]
        public int Seats { get; set; } = 120;

        [Url, Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = "https://picsum.photos/seed/cinema/800/450";

        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }
}
