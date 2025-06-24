using System.ComponentModel.DataAnnotations;

namespace EasypayBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}