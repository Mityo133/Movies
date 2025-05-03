using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies;
using Movies.Controllers;
using Movies.Data;
using Movies.Models;
using Xunit;

namespace MoviesTestProject

{
    public class MoviesControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesTestDb")
                .Options;

            var context = new ApplicationDbContext(options);

            // Seed test data only once
            if (!context.Movie.Any())
            {
                var genre = new Genres { Id = 1, Name = "Action" };
                var actor = new Actor { Id = 1, FirstName = "Tom",LastName= "Cruise" };
                var movie = new Movie
                {
                    Id = 1,
                    Name = "Mission Impossible",
                    GenreId = 1,
                    Genre = genre,
                    MovieActors = new List<MovieActors>
                {
                    new MovieActors { MovieId = 1, ActorId = 1, Actor = actor }
                }
                };

                context.Genres.Add(genre);
                context.Actor.Add(actor);
                context.Movie.Add(movie);
                context.SaveChanges();
            }

            return context;
        }

        [Fact]
        public async Task Details_ReturnsMovie_WhenMovieExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MoviesController(context);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Movie>(viewResult.Model);

            Assert.Equal("Mission Impossible", model.Name);
            Assert.Equal("Action", model.Genre.Name);
            Assert.Single(model.MovieActors);
            Assert.Equal("Tom Cruise", model.MovieActors.First().Actor.Name);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var context = GetInMemoryDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenMovieDoesNotExist()
        {
            var context = GetInMemoryDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Details(999); // non-existent ID

            Assert.IsType<NotFoundResult>(result);
        }
    }
}