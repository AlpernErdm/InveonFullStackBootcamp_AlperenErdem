using Microsoft.AspNetCore.Identity;

namespace LibraryWebApplication.Models
{
    public class AppUser:IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string FullName=>string.Join(" ", Name, Lastname);    
    }
}
