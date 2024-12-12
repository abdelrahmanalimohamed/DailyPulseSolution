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
    [Migration("20241119092521_DropScopeOfWorkIdFkFromProjectTableAndCreateNewBridgeTableForProjectsAndScopes")]
    partial class DropScopeOfWorkIdFkFromProjectTableAndCreateNewBridgeTableForProjectsAndScopes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DailyPulse.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("current_timestamp()");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
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

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ReportToId");

                    b.ToTable("Employees");
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

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("RegionId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.ProjectsScopes", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ScopeOfWorkId")
                        .HasColumnType("char(36)");

                    b.HasKey("ProjectId", "ScopeOfWorkId");

                    b.HasIndex("ScopeOfWorkId");

                    b.ToTable("ProjectsScopes");
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

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

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

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ScopeId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ScopeId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskDetail", b =>
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

                    b.ToTable("TaskDetails");
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

            modelBuilder.Entity("DailyPulse.Domain.Entities.ProjectsScopes", b =>
                {
                    b.HasOne("DailyPulse.Domain.Entities.Project", "Project")
                        .WithMany("ProjectsScopes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DailyPulse.Domain.Entities.ScopeOfWork", "ScopeOfWork")
                        .WithMany("ProjectsScopes")
                        .HasForeignKey("ScopeOfWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("ScopeOfWork");
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

                    b.HasOne("DailyPulse.Domain.Entities.ScopeOfWork", "Scope")
                        .WithMany("Tasks")
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.TaskDetail", b =>
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

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Location", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Project", b =>
                {
                    b.Navigation("ProjectsScopes");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Region", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.ScopeOfWork", b =>
                {
                    b.Navigation("ProjectsScopes");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DailyPulse.Domain.Entities.Task", b =>
                {
                    b.Navigation("ReAssigns");

                    b.Navigation("TaskDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
