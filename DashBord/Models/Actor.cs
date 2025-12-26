using System.ComponentModel.DataAnnotations;

namespace DashBourd.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }

        public ICollection<Movie>? Movies { get; set; }
        public ICollection<MovieActor>? MovieActors { get; set; }

    }
}
