namespace CLDV_POE_1_Example_Movie_Database.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Screening
    {
        [Key]
        public int ScreeningId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Today.AddHours(18);

        [Required, DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; } = DateTime.Today.AddHours(20);

        // Foreign Keys
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Display(Name = "Cinema")]
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }
    }
}
