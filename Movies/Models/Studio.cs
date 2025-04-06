namespace Movies.Models
{
    public class Studio
    {
        public int Id { get; set; }

        public string DirectorName { get; set; }

        public int NumberOfStaff {  get; set; }

        public Movie? Movie { get; set; }
    }
}
