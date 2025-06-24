using EasypayBackend.Data;
using EasypayBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EasypayBackend.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EasypayDbContext context) : base(context)
        {
        }

        public async Task<Employee> GetByUserIdAsync(int userId)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.UserId == userId);
        }
    }
}