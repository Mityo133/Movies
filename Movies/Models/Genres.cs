namespace Movies.Models
{
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
