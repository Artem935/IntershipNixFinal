
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Bursday { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}