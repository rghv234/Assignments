using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo1.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username should not be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password should not be empty")]

        public string Password { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email should not be empty")]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
