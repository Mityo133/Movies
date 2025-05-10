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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<MovieActors>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

   
        }

        public DbSet<Actor> Actor { get; set; } = default!;
        public DbSet<Genres> Genres { get; set; } = default!;
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<MovieActors> MovieActors { get; set; } = default!;

    }
}
