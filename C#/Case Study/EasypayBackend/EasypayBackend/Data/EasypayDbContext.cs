using EasypayBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EasypayBackend.Data
{
    public class EasypayDbContext : DbContext
    {
        public EasypayDbContext(DbContextOptions<EasypayDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Employee and LeaveRequest
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LeaveRequestsAsEmployee)
                .WithOne(lr => lr.Employee)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the many-to-one relationship between LeaveRequest and User (Manager)
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Manager)
                .WithMany() // Manager may handle multiple leave requests, but no inverse navigation yet
                .HasForeignKey(lr => lr.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete on Manager to prevent data loss

            // Ensure all entities have primary keys
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Payroll>().HasKey(p => p.Id);
            modelBuilder.Entity<LeaveRequest>().HasKey(lr => lr.Id);
            modelBuilder.Entity<AuditLog>().HasKey(al => al.Id);
        }
    }
}