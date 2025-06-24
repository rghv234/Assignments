using EasypayBackend.Data;
using EasypayBackend.Dtos;
using EasypayBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasypayBackend.Controllers
{
    [Route("api/payroll")]
    [ApiController]
    [Authorize(Roles = "PayrollProcessor,Employee,Manager")]
    public class PayrollController : ControllerBase
    {
        private readonly EasypayDbContext _context;

        public PayrollController(EasypayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "PayrollProcessor,Manager")]
        public async Task<IActionResult> GetPayrolls()
        {
            var payrolls = await _context.Payrolls
                .Select(p => new PayrollDto
                {
                    Id = p.Id,
                    EmployeeId = p.EmployeeId,
                    PayPeriod = p.PayPeriod,
                    GrossSalary = p.GrossSalary,
                    Deductions = p.Deductions,
                    NetSalary = p.NetSalary,
                    Status = p.Status
                })
                .ToListAsync();
            return Ok(payrolls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayroll(int id)
        {
            var payroll = await _context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.Id == id);
            if (payroll == null) return NotFound(new { Message = "Payroll not found" });

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (User.IsInRole("Employee") && payroll.Employee.UserId != currentUserId)
                return Forbid();

            return Ok(new PayrollDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                PayPeriod = payroll.PayPeriod,
                GrossSalary = payroll.GrossSalary,
                Deductions = payroll.Deductions,
                NetSalary = payroll.NetSalary,
                Status = payroll.Status
            });
        }

        [HttpPost("calculate")]
        [Authorize(Roles = "PayrollProcessor")]
        public async Task<IActionResult> CalculatePayroll([FromBody] PayrollDto payrollDto)
        {
            var employee = await _context.Employees.FindAsync(payrollDto.EmployeeId);
            if (employee == null) return NotFound(new { Message = "Employee not found" });

            // Simple calculation logic (replace with actual tax/compliance logic)
            var grossSalary = employee.Salary;
            var deductions = grossSalary * 0.2m; // Example: 20% deductions
            var netSalary = grossSalary - deductions;

            var payroll = new Payroll
            {
                EmployeeId = payrollDto.EmployeeId,
                PayPeriod = payrollDto.PayPeriod,
                GrossSalary = grossSalary,
                Deductions = deductions,
                NetSalary = netSalary,
                Status = "Processed"
            };

            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();
            return Ok(new PayrollDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                PayPeriod = payroll.PayPeriod,
                GrossSalary = payroll.GrossSalary,
                Deductions = payroll.Deductions,
                NetSalary = payroll.NetSalary,
                Status = payroll.Status
            });
        }

        [HttpPost("{id}/verify")]
        [Authorize(Roles = "PayrollProcessor")]
        public async Task<IActionResult> VerifyPayroll(int id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll == null) return NotFound(new { Message = "Payroll not found" });
            if (payroll.Status != "Processed") return BadRequest(new { Message = "Payroll not in Processed state" });

            payroll.Status = "Verified";
            await _context.SaveChangesAsync();
            return Ok(new PayrollDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                PayPeriod = payroll.PayPeriod,
                GrossSalary = payroll.GrossSalary,
                Deductions = payroll.Deductions,
                NetSalary = payroll.NetSalary,
                Status = payroll.Status
            });
        }

        [HttpPost("{id}/disburse")]
        [Authorize(Roles = "PayrollProcessor")]
        public async Task<IActionResult> DisbursePayroll(int id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll == null) return NotFound(new { Message = "Payroll not found" });
            if (payroll.Status != "Verified") return BadRequest(new { Message = "Payroll not in Verified state" });

            payroll.Status = "Disbursed";
            await _context.SaveChangesAsync();
            return Ok(new PayrollDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                PayPeriod = payroll.PayPeriod,
                GrossSalary = payroll.GrossSalary,
                Deductions = payroll.Deductions,
                NetSalary = payroll.NetSalary,
                Status = payroll.Status
            });
        }
    }
}