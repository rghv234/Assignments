using System.ComponentModel.DataAnnotations;
namespace EF_CodeFirst_Demo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();    
    }
}
