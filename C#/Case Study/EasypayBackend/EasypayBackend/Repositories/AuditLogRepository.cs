using EasypayBackend.Data;
using EasypayBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EasypayBackend.Repositories
{
    public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(EasypayDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId)
        {
            return await _context.AuditLogs.Where(al => al.UserId == userId).ToListAsync();
        }
    }
}