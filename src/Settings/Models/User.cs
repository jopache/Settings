using Microsoft.AspNetCore.Identity;

namespace Settings.Models
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}