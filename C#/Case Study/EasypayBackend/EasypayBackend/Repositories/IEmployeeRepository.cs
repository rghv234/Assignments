using EasypayBackend.Models;
using System.Linq.Expressions;

namespace EasypayBackend.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetByUserIdAsync(int userId);
        Task<bool> AnyAsync(Expression<Func<Employee, bool>> predicate);
        Task SaveChangesAsync();
    }
}