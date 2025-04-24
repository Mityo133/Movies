using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Controllers;
using Movies.Data;
using Movies.Models;
using System.Threading.Tasks;

public class MovieControllerTests
{
    [Test]
    public async Task Details_ReturnsViewResult_WithMovie()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieTestDb")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var movie = new Movie { Id = 1, Name = "Inception" };
            context.Movie.Add(movie);
            context.SaveChanges();

            var controller = new MoviesController(context);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<Movie>(viewResult.Model);
            Xunit.Assert.Equal("Inception", model.Name);
        }
    }
}

