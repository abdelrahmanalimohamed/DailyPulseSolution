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
        public DbSet<TaskDetail> TaskDetails { get; set; }
        public DbSet<ScopeOfWork> ScopeOfWorks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ReAssign> ReAssigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(k => k.Id);

                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role)
                    .IsRequired();

                entity.Property(e => e.IsAdmin)
                    .HasDefaultValue(false);

                // Self-referencing relationship for reporting structure
                entity.HasOne(e => e.ReportTo)
                    .WithMany(e => e.DirectReports)
                    .HasForeignKey(e => e.ReportToId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 1:* Relationship with Projects
                entity.HasMany(e => e.Projects)
                    .WithOne(p => p.TeamLead)
                    .HasForeignKey(p => p.TeamLeadId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Tasks)
                   .WithOne(p => p.Employee)
                   .HasForeignKey(p => p.EmpId)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            // Project Entity Configuration
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(k => k.Id);

                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");
                entity.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(500);

                entity.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                // Foreign Key Relationships
                entity.HasOne(p => p.ScopeOfWork)
                    .WithMany(s => s.Projects) // Updated to reference Projects
                    .HasForeignKey(p => p.ScopeOfWorkId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Region)
                    .WithMany(s => s.Projects)
                    .HasForeignKey(p => p.RegionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Location)
                    .WithMany(s => s.Projects)
                    .HasForeignKey(p => p.LocationId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Foreign Key for Team Lead
                entity.HasOne(p => p.TeamLead)
                    .WithMany(e => e.Projects)
                    .HasForeignKey(p => p.TeamLeadId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Tasks Entity Configuration
            modelBuilder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

                entity.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(500);

                entity.Property(p => p.Area)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.DrawingId)
                   .IsRequired()
                   .HasMaxLength(500);

                entity.Property(p => p.DrawingTitle)
                 .IsRequired()
                 .HasMaxLength(500);

                entity.Property(p => p.FilePath)
                   .IsRequired()
                   .HasMaxLength(500);

                entity.Property(p => p.DateFrom)
                     .IsRequired();

                entity.Property(p => p.DateTo)
                     .IsRequired();

                // Foreign Key Relationships
                entity.HasOne(p => p.Scope)
                    .WithMany(s => s.Tasks) // Updated to reference Projects
                    .HasForeignKey(p => p.ScopeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Project)
                    .WithMany(s => s.Tasks)
                    .HasForeignKey(p => p.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Foreign Key for Employee
                entity.HasOne(p => p.Employee)
                    .WithMany(e => e.Tasks)
                    .HasForeignKey(p => p.EmpId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Define one-to-many relationship with TaskDetail
                entity.HasMany(t => t.TaskDetails)
                      .WithOne(td => td.Task)
                      .HasForeignKey(td => td.TaskId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskDetail>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

                entity.HasOne(p => p.Task)
                    .WithMany(r => r.TaskDetails)
                    .HasForeignKey(x => x.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");
                entity.Property(l => l.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                // Location belongs to a Region
                entity.HasOne(p => p.Region)
                    .WithMany(r => r.Locations)
                    .HasForeignKey(x => x.RegionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                // 1:* Relationship with Locations
                entity.HasMany(r => r.Locations)
                    .WithOne(x => x.Region)
                    .HasForeignKey(u => u.RegionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ScopeOfWork>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                // 1:* Relationship with Projects
                entity.HasMany(s => s.Projects)
                    .WithOne(p => p.ScopeOfWork)
                    .HasForeignKey(p => p.ScopeOfWorkId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ReAssign>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

                entity.HasOne(r => r.Employee)
                      .WithMany(e => e.ReAssigns)
                      .HasForeignKey(r => r.EmpId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

                entity.HasOne(r => r.TeamLead)
                    .WithMany()
                    .HasForeignKey(r => r.TeamLeadId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

                entity.HasOne(r => r.Task)
                    .WithMany(t => t.ReAssigns)
                    .HasForeignKey(r => r.TaskId)
                    .OnDelete(DeleteBehavior.Cascade); // Adjust as necessary
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
