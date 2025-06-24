namespace EasypayBackend.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Denied
        public Employee Employee { get; set; }
        public Employee Manager { get; set; }
    }
}
