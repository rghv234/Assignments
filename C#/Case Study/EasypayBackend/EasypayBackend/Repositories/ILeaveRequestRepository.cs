using EasypayBackend.Models;

namespace EasypayBackend.Repositories
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<LeaveRequest>> GetByManagerIdAsync(int managerId);
    }
}