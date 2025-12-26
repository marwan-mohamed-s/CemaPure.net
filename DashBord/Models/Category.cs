using System.ComponentModel.DataAnnotations;

namespace DashBourd.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Movie>? Movies { get; set; }
    }
}
