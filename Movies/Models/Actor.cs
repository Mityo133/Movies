namespace Movies.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Image { get; set; }
         public ICollection<MovieActors>? MovieActors { get; set; } = new List<MovieActors>();
    }
}
