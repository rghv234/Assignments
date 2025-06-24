using EasypayBackend.Models;

namespace EasypayBackend.Repositories
{
    public interface IPayrollRepository : IGenericRepository<Payroll>
    {
        Task<IEnumerable<Payroll>> GetByEmployeeIdAsync(int employeeId);
    }
}