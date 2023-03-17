using Microsoft.AspNetCore.Identity;

namespace PresentationMVC.Models
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
    }
}