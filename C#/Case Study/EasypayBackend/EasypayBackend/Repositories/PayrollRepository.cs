using EasypayBackend.Data;
using EasypayBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EasypayBackend.Repositories
{
    public class PayrollRepository : GenericRepository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(EasypayDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Payroll>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Payrolls.Where(p => p.EmployeeId == employeeId).ToListAsync();
        }
    }
}