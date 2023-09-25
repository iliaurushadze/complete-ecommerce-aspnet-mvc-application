namespace eTickets.Models.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Directors = new List<Director>();
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();
        }

        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Cinema> Cinemas { get; set; }
    }
}
