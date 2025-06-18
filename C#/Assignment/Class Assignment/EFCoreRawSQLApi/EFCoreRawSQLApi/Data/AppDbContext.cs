using Microsoft.EntityFrameworkCore;
using EFCoreRawSQLApi.Models;

namespace EFCoreRawSQLApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}
