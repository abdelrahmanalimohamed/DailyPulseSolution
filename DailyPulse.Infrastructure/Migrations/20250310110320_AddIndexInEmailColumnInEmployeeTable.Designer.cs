﻿// <auto-generated />
using System;
using DailyPulse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250310110320_AddIndexInEmailColumnInEmployeeTable")]
    partial class AddIndexInEmailColumnInEmployeeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DailyPulse.Domain.Entities.AdminRejectedTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ClosedComments")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("AdminRejectedTasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ReportToId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ReportToId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.EmployeeRejectedTasks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RejectionReasons")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("EmpId");

                    b.HasIndex("TaskId");

                    b.ToTable("EmployeeRejectedTasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Trade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.ReAssign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeamLeadId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("EmpId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TeamLeadId");

                    b.ToTable("ReAssigns");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.ScopeOfWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ScopeOfWorks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DrawingId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("DrawingTitle")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EstimatedWorkingHours")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsRejectedByAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsRejectedByEmployee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("Levels")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskNewRequirements", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<string>("EstimatedWorkingHours")
                        .HasColumnType("longtext");

                    b.Property<string>("RequirementsDetails")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskNewRequirements");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskStatusLogs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<int>("NewStatus")
                        .HasColumnType("int");

                    b.Property<int>("OldStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskStatusLogs");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskWorkLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LogDesc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PauseTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskWorkLogs");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.AdminRejectedTask", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Task", "Task")
                        .WithMany("TaskLogs")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Employee", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Employee", "ReportTo")
                        .WithMany("DirectReports")
                        .HasForeignKey("ReportToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReportTo");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.EmployeeRejectedTasks", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Employee", "Employee")
                        .WithMany("RejectedTasks")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DailyPulse.Domain.Entities.Task", "Task")
                        .WithMany("RejectedTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Location", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Region", "Region")
                        .WithMany("Locations")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Project", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Location", "Location")
                        .WithMany("Projects")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DailyPulse.Domain.Entities.Region", "Region")
                        .WithMany("Projects")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.ReAssign", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Employee", "Employee")
                        .WithMany("ReAssigns")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DailyPulse.Domain.Entities.Task", "Task")
                        .WithMany("ReAssigns")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DailyPulse.Domain.Entities.Employee", "TeamLead")
                        .WithMany()
                        .HasForeignKey("TeamLeadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Task");

                    b.Navigation("TeamLead");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Task", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Employee", "Employee")
                        .WithMany("Tasks")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DailyPulse.Domain.Entities.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskNewRequirements", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Task", "Task")
                        .WithMany("TaskNewRequirements")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskStatusLogs", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Task", "Task")
                        .WithMany("TaskStatusLogs")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskWorkLog", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Task", "Task")
                        .WithMany("TaskDetails")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Employee", b =>
                {
                    b.Navigation("DirectReports");

                    b.Navigation("ReAssigns");

                    b.Navigation("RejectedTasks");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Location", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Region", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Task", b =>
                {
                    b.Navigation("ReAssigns");

                    b.Navigation("RejectedTasks");

                    b.Navigation("TaskDetails");

                    b.Navigation("TaskLogs");

                    b.Navigation("TaskNewRequirements");

                    b.Navigation("TaskStatusLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
