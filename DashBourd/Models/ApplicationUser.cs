using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DashBourd.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }= String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string? Address { get; set; }
    }
}
