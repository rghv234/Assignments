using System.ComponentModel.DataAnnotations;
namespace DeptAPIDemo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
