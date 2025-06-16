using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username should not be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        public string Password { get; set; }
    }
}
