using EasypayBackend.Data;
using EasypayBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EasypayBackend.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(EasypayDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.LeaveRequests.Where(lr => lr.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetByManagerIdAsync(int managerId)
        {
            return await _context.LeaveRequests.Where(lr => lr.ManagerId == managerId).ToListAsync();
        }
    }
}