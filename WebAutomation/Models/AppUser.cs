using Microsoft.AspNetCore.Identity;

namespace WebAutomation.Models
{
    public class AppUser : IdentityUser
    {
       
        public string? FullName { get; set; }
    }
}
