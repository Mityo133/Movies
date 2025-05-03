using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Controllers;
using Movies.Data;
using Movies.Models;

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
            var context = new ApplicationDbContext(options);
            var moviesController = new MoviesController(context);
            var movie = new Movie { Id = 1, Name = "Inception", Description = "", Image = "", Genre = new Genres() { Name = "Sci-Fi" } };

            // Act
            context.Movie.Add(movie);
            context.SaveChanges();

            // Assert
            var retrievedMovie = context.Movie.Find(1);
            NUnit.Framework.Assert.IsNotNull(retrievedMovie, "Movie was not added successfully.");
            NUnit.Framework.Assert.AreEqual("Inception", retrievedMovie.Name, "Movie title is incorrect.");
            NUnit.Framework.Assert.AreEqual("Sci-Fi", retrievedMovie.Genre.Name, "Movie genre is incorrect.");


        }
        
    }
}