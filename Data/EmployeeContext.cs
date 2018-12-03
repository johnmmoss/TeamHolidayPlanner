using Microsoft.EntityFrameworkCore;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
            // Force database
            //Database.EnsureCreated();

            //Database.Migrate();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmploymentGrade> EmploymentGrade { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<HolidayPeriod> HolidayPeriod{ get; set; }
        public DbSet<EmployeeHoliday> EmployeeHoliday { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup the many-to-many relationship for role/permissions
            modelBuilder.Entity<RolePermission>()
                .HasKey(bc => new { bc.RoleID, bc.PermissionID });
            modelBuilder.Entity<RolePermission>()
                .HasOne(bc => bc.Role)
                .WithMany(b => b.RolePermissions)
                .HasForeignKey(bc => bc.RoleID);
            modelBuilder.Entity<RolePermission>()
                .HasOne(bc => bc.Permission)
                .WithMany(c => c.RolePermissions)
                .HasForeignKey(bc => bc.PermissionID);
        }
    }
}
