using System.ComponentModel.DataAnnotations;

namespace EasypayBackend.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string TaxWithholding { get; set; }
        public decimal Salary { get; set; }
        public int LeaveBalance { get; set; }

        // Navigation property for LeaveRequests
        public virtual ICollection<LeaveRequest> LeaveRequestsAsEmployee { get; set; }
    }
}