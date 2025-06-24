namespace EasypayBackend.Dtos
{
    public class RegisterWithEmployeeDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; } // Optional name for employee
    }
}