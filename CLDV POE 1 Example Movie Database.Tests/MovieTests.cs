namespace CLDV_POE_1_Example_Movie_Database.Tests
{
    using CLDV_POE_1_Example_Movie_Database.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for the Movie model.
    /// Demonstrates: Model instantiation, property validation, and collection initialization.
    /// </summary>
    public class MovieTests
    {
        [Fact]
        public void Movie_Constructor_InitializesProperties()
        {
            // Arrange
            var title = "Inception";
            var genre = "Sci-Fi";
            var releaseDate = new DateTime(2010, 7, 16);
            var posterUrl = "https://example.com/poster.jpg";

            // Act
            var movie = new Movie
            {
                MovieId = 1,
                Title = title,
                Genre = genre,
                ReleaseDate = releaseDate,
                PosterUrl = posterUrl
            };

            // Assert
            Assert.Equal(1, movie.MovieId);
            Assert.Equal(title, movie.Title);
            Assert.Equal(genre, movie.Genre);
            Assert.Equal(releaseDate, movie.ReleaseDate);
            Assert.Equal(posterUrl, movie.PosterUrl);
        }

        [Fact]
        public void Movie_ScreeningsCollection_InitializedAsEmpty()
        {
            // Arrange & Act
            var movie = new Movie { Title = "Test Movie", Genre = "Drama" };

            // Assert
            Assert.NotNull(movie.Screenings);
            Assert.Empty(movie.Screenings);
        }

        [Fact]
        public void Movie_Title_DefaultsToEmptyString()
        {
            // Arrange & Act
            var movie = new Movie();

            // Assert
            Assert.NotNull(movie.Title);
            Assert.Equal(string.Empty, movie.Title);
        }

        [Fact]
        public void Movie_PosterUrl_DefaultsToPlaceholder()
        {
            // Arrange & Act
            var movie = new Movie();

            // Assert
            Assert.Equal("https://picsum.photos/seed/movie/600/900", movie.PosterUrl);
        }
    }
}
