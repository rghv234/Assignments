using EasypayBackend.Models;

namespace EasypayBackend.Repositories
{
    public interface IAuditLogRepository : IGenericRepository<AuditLog>
    {
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);
        Task SaveChangesAsync();
    }
}