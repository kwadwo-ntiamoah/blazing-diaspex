using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? AuthenticationType { get; set; }
    }
}