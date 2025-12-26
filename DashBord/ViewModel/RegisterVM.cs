using System.ComponentModel.DataAnnotations;

namespace DashBourd.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public String FirstName { get; set; } = String.Empty;
        [Required]
        public String LastName { get; set; } = String.Empty;
        [Required]
        public String UserName { get; set; } = String.Empty;
        [Required,EmailAddress]
        public String Email { get; set; } = String.Empty;
        [Required, MinLength(6),DataType(DataType.Password)]
        public String Password { get; set; } = String.Empty;
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public String ConfermPassword { get; set; } = String.Empty;
    }
}
