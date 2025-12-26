using DashBourd.Models;

namespace DashBourd.ViewModel
{
    public class MovieVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Cinema> Cinemas { get; set; }
        public Movie? Movie { get; set; }
    }
}
