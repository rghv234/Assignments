namespace EasypayBackend.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string TaxWithholding { get; set; }
        public decimal Salary { get; set; }
        public int LeaveBalance { get; set; }
    }
}
