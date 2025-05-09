using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using System.Linq;
namespace Movies.Services.Abstractions
{
     public interface IMovieService
    {
        // Define the methods that will be implemented in the MovieService class
        Task<IEnumerable<Movie>> AllMoviesAsync { get; }

        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchString);
        Task<IEnumerable<Movie>> FilterMoviesByYearAsync(int year);
        Task<IEnumerable<Movie>> SortMoviesAsync(string sortOption);
    }
}
