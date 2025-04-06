using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movies.Models.Actor> Actor { get; set; } = default!;
        public DbSet<Movies.Models.Genres> Genres { get; set; } = default!;
        public DbSet<Movies.Models.Movie> Movie { get; set; } = default!;
        public DbSet<Movies.Models.MovieActors> MovieActors { get; set; } = default!;
        public DbSet<Movies.Models.Studio> Studios { get; set; } = default!;
    }
}
