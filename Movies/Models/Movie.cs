namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public double Ratings { get; set; }

        public int GenreId { get; set; }           // Foreign Key
        public Genres? Genre { get; set; }         // Navigation Property

        public string Image { get; set; }

        public ICollection<MovieActors>? MovieActors { get; set; } = new List<MovieActors>();// Many-to-Many
    }
}
