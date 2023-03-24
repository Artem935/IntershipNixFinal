using Microsoft.AspNetCore.Identity;

namespace PresentationMVC.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime date { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }

        public User() { }
        public User(string name, string surname, DateTime date, string password, string comfirmPassword)
        {
            Name = name;
            Surname = surname;
            this.date = date;
        }
    }
}