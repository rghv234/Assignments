using Microsoft.EntityFrameworkCore;
using SimpleAPIDemo.Models; 
namespace SimpleAPIDemo.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
