using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskWorkLog> TaskWorkLogs { get; set; }
        public DbSet<ScopeOfWork> ScopeOfWorks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ReAssign> ReAssigns { get; set; }
        public DbSet<EmployeeRejectedTasks> EmployeeRejectedTasks { get; set; }
        public DbSet<TaskNewRequirements> TaskNewRequirements { get; set; }
        public DbSet<AdminRejectedTask> AdminRejectedTasks { get; set; }
        public DbSet<TaskStatusLogs> TaskStatusLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}