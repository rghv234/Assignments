using EasypayBackend.Models;
using System.Linq.Expressions;

namespace EasypayBackend.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
        Task SaveChangesAsync();
    }
}