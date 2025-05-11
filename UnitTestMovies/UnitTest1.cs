namespace Movies.Tests
{
    public class MoviesControllerTests
    {
        [Test]
        public async Task AddMovie_ShouldAddMovieSuccessfullyAsync()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var moviesService = new Movies.Services.MoviesService(context);
                var controller = new MoviesController(moviesService);

                var movie = new Movie
                {
                    Name = "Test Movie",
                    ReleaseYear = 2023,
                    Description = "Test Description",
                    Ratings = 5,
                    GenreId = 1,
                    Image = "test.jpg",
                    Trailer = "test.mp4",
                    Views = 100
                };

                // Act
                var result = await controller.Create(movie);

                // Assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
            }
        }
        
    }
}
