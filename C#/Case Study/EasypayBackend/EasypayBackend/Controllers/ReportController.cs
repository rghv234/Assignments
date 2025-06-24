using EasypayBackend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasypayBackend.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly EasypayDbContext _context;

        public ReportController(EasypayDbContext context)
        {
            _context = context;
        }

        [HttpGet("compliance")]
        public async Task<IActionResult> GetComplianceReport()
        {
            var payrolls = await _context.Payrolls
                .Include(p => p.Employee)
                .Where(p => p.Status == "Disbursed")
                .Select(p => new
                {
                    EmployeeName = p.Employee.Name,
                    PayPeriod = p.PayPeriod,
                    GrossSalary = p.GrossSalary,
                    Deductions = p.Deductions,
                    NetSalary = p.NetSalary,
                    TaxWithholding = p.Employee.TaxWithholding
                })
                .ToListAsync();

            return Ok(payrolls);
        }

        [HttpGet("audit")]
        public async Task<IActionResult> GetAuditLogs()
        {
            var logs = await _context.AuditLogs
                .Include(a => a.User)
                .Select(a => new
                {
                    a.Id,
                    UserEmail = a.User.Email,
                    a.Action,
                    a.Timestamp,
                    a.Details
                })
                .ToListAsync();
            return Ok(logs);
        }
    }
}