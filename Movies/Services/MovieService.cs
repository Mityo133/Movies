using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.Services.Abstractions;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {

        public Task<IEnumerable<Movie>> AllMoviesAsync => throw new NotImplementedException();

        public Task AddMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> FilterMoviesByYearAsync(int year)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> SearchMoviesAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> SortMoviesAsync(string sortOption)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }

    
    
}
