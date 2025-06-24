using EasypayBackend.Data;
using EasypayBackend.Dtos;
using EasypayBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Asp.Versioning;

namespace EasypayBackend.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/leaves")]
    [ApiController]
    [Authorize(Roles = "Employee,Manager")]
    public class LeaveController : ControllerBase
    {
        private readonly EasypayDbContext _context;

        public LeaveController(EasypayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveRequests()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }
            var currentUserId = int.Parse(userIdClaim.Value);
            var query = _context.LeaveRequests.AsQueryable();

            if (User.IsInRole("Employee"))
            {
                query = query.Where(lr => lr.Employee.UserId == currentUserId);
            }
            else if (User.IsInRole("Manager"))
            {
                query = query.Where(lr => lr.Manager.UserId == currentUserId);
            }

            var leaves = await query
                .Select(lr => new LeaveRequestDto
                {
                    Id = lr.Id,
                    EmployeeId = lr.EmployeeId,
                    ManagerId = lr.ManagerId,
                    StartDate = lr.StartDate,
                    EndDate = lr.EndDate,
                    Status = lr.Status
                })
                .ToListAsync();
            return Ok(leaves);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDto leaveDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }
            var currentUserId = int.Parse(userIdClaim.Value);

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == currentUserId);
            if (employee == null) return NotFound(new { Message = "Employee not found" });

            var leaveRequest = new LeaveRequest
            {
                EmployeeId = employee.Id,
                ManagerId = leaveDto.ManagerId,
                StartDate = leaveDto.StartDate,
                EndDate = leaveDto.EndDate,
                Status = "Pending"
            };

            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return Ok(new LeaveRequestDto
            {
                Id = leaveRequest.Id,
                EmployeeId = leaveRequest.EmployeeId,
                ManagerId = leaveRequest.ManagerId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Status = leaveRequest.Status
            });
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ApproveLeave(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }
            var currentUserId = int.Parse(userIdClaim.Value);

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null) return NotFound(new { Message = "Leave request not found" });
            if (leaveRequest.Status != "Pending") return BadRequest(new { Message = "Leave not in Pending state" });

            var manager = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == currentUserId);
            if (manager == null || leaveRequest.ManagerId != manager.Id) return Forbid();

            leaveRequest.Status = "Approved";
            var employee = await _context.Employees.FindAsync(leaveRequest.EmployeeId);
            employee.LeaveBalance -= (leaveRequest.EndDate - leaveRequest.StartDate).Days;
            await _context.SaveChangesAsync();
            return Ok(new LeaveRequestDto
            {
                Id = leaveRequest.Id,
                EmployeeId = leaveRequest.EmployeeId,
                ManagerId = leaveRequest.ManagerId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Status = leaveRequest.Status
            });
        }

        [HttpPost("{id}/deny")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DenyLeave(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }
            var currentUserId = int.Parse(userIdClaim.Value);

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null) return NotFound(new { Message = "Leave request not found" });
            if (leaveRequest.Status != "Pending") return BadRequest(new { Message = "Leave not in Pending state" });

            var manager = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == currentUserId);
            if (manager == null || leaveRequest.ManagerId != manager.Id) return Forbid();

            leaveRequest.Status = "Denied";
            await _context.SaveChangesAsync();
            return Ok(new LeaveRequestDto
            {
                Id = leaveRequest.Id,
                EmployeeId = leaveRequest.EmployeeId,
                ManagerId = leaveRequest.ManagerId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Status = leaveRequest.Status
            });
        }
    }
}