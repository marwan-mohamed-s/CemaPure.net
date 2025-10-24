using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace DashBourd.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        [MinLength(5)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }

        public bool Status { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        public string? MainImage { get; set; }

        public List<string>? SubImages { get; set; }



        // العلاقات
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }

        public ICollection<Actor>? Actors { get; set; }
        public ICollection<MovieActor>? MovieActors { get; set; }

    }
}
