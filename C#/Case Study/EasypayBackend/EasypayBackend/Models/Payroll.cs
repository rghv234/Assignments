namespace EasypayBackend.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PayPeriod { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public string Status { get; set; } // Processed, Verified, Disbursed
        public Employee Employee { get; set; }
    }
}
