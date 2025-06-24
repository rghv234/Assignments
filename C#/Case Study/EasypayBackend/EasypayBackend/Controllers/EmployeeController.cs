using EasypayBackend.Dtos;
using EasypayBackend.Models;
using EasypayBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Asp.Versioning;

namespace EasypayBackend.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/employees")]
    [ApiVersion("1.0")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID claim not found"));
            if (User.IsInRole("Employee") && employee.UserId != userId)
                return Forbid();

            return Ok(new EmployeeDto
            {
                Id = employee.Id,
                UserId = employee.UserId,
                Name = employee.Name,
                ContactInfo = employee.ContactInfo,
                TaxWithholding = employee.TaxWithholding,
                Salary = employee.Salary,
                LeaveBalance = employee.LeaveBalance
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                UserId = employeeDto.UserId,
                Name = employeeDto.Name,
                ContactInfo = employeeDto.ContactInfo,
                TaxWithholding = employeeDto.TaxWithholding,
                Salary = employeeDto.Salary,
                LeaveBalance = employeeDto.LeaveBalance
            };

            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            return Ok(new EmployeeDto
            {
                Id = employee.Id,
                UserId = employee.UserId,
                Name = employee.Name,
                ContactInfo = employee.ContactInfo,
                TaxWithholding = employee.TaxWithholding,
                Salary = employee.Salary,
                LeaveBalance = employee.LeaveBalance
            });
        }
    }
}