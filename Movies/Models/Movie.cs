namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        private double _ratings;
        public double Ratings
        {
            get => _ratings;
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _ratings = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Ratings must be between 0 and 10.");
                }

            }
        }


        public int GenreId { get; set; }           // Foreign Key
        public Genres? Genre { get; set; }         // Navigation Property

        public string Image { get; set; }

        public string Trailer { get; set; }

        public ICollection<MovieActors>? MovieActors { get; set; } = new List<MovieActors>();// Many-to-Many
    }
}
