using Movies.Models;
namespace Movies.Services.Abstractions
{
    public interface IMoviesSirvice
    {
        Task<Movie> Details(int Id);
        Task<ICollection<Movie>> GetAllAsync();
        Task Create(Movie movie);
        Task Edit(int? id);
        Task Delete(int Id);
        Task Catalog(string nameFilter, int? yearFilter, string? sortbyName);
        ICollection<Movie> GetByName(string name);

        
    }
}
