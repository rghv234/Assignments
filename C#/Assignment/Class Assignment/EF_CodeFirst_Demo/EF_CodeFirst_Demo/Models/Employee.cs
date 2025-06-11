using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_CodeFirst_Demo.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name should not be Blank")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Gender should not be blank")]
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender should be 'Male', 'Female', or 'Other'")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Salary should not be blank")]
        [Range(3000, 1000000, ErrorMessage = "Salary must be between 3,000 and 1,000,000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Location should not be blank")]
        //[StringLength(100, ErrorMessage = "Location should not exceed 100 characters")]
        public string Location { get; set; }


        [Required(ErrorMessage = "Email should not be Blank")]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter a valid email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Joining should not be blank")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Password Should Not Be Blank")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password Should contain minimum 8 chars or max 30 chars")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
        //ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Should not be Blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password should match with the Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

    }
}
