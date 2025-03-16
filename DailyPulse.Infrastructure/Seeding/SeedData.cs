using BCrypt.Net;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using DailyPulse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Infrastructure.Seeding
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            if (!context.Regions.Any())
            {
                await context.Regions.AddRangeAsync(
                    new Region { Name = "EGYPT" },
                    new Region { Name = "KSA" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Locations.Any())
            {
                var regionEgypt = await context.Regions.Where(x => x.Name == "EGYPT")
                                    .Select(a => a.Id).FirstOrDefaultAsync();

                var regionKSA = await context.Regions.Where(x => x.Name == "KSA")
                                    .Select(a => a.Id).FirstOrDefaultAsync();

                await context.Locations.AddRangeAsync(
                    new Location { Name = "Headquarters", RegionId = regionEgypt },
                    new Location { Name = "AlAlamein City University (Finishing)", RegionId = regionEgypt },
                    new Location { Name = "AlAlamein New Towers LD04 - Civil", RegionId = regionEgypt },
                    new Location { Name = "AlAlamein New Towers LD04 - Finishing", RegionId = regionEgypt },
                    new Location { Name = "AlAlamein Towers LD07 - Finishing", RegionId = regionEgypt },
                    new Location { Name = "Commercial Center at Suez Road", RegionId = regionEgypt },
                    new Location { Name = "ElAhly Bank", RegionId = regionEgypt },
                    new Location { Name = "ElDabaa - 300k Acre", RegionId = regionEgypt },
                    new Location { Name = "ElDabaa - Mostakbal Masr", RegionId = regionEgypt },
                    new Location { Name = "ElMinya Hospital", RegionId = regionEgypt },
                    new Location { Name = "Faisal Islamic Bank", RegionId = regionEgypt },
                    new Location { Name = "HSR Admin Building - New Capital", RegionId = regionEgypt },
                    new Location { Name = "HSR Station", RegionId = regionEgypt },
                    new Location { Name = "Light Maintenance Workshop", RegionId = regionEgypt },
                    new Location { Name = "LRT Station", RegionId = regionEgypt },
                    new Location { Name = "Luxor HSR Station", RegionId = regionEgypt },
                    new Location { Name = "Mansoura 8", RegionId = regionEgypt },
                    new Location { Name = "Military Institutes Command", RegionId = regionEgypt },
                    new Location { Name = "Ministry of Foreign Affairs Amendments", RegionId = regionEgypt },
                    new Location { Name = "New Capital Area Management", RegionId = regionEgypt },
                    new Location { Name = "New Capital Central Workshop", RegionId = regionEgypt },
                    new Location { Name = "Nile Tower (N31)", RegionId = regionEgypt },
                    new Location { Name = "QNB Bank", RegionId = regionEgypt },
                    new Location { Name = "SECON Towers", RegionId = regionEgypt },
                    new Location { Name = "SIAC New Capital HQ", RegionId = regionEgypt },
                    new Location { Name = "Suez Workshops & Warehouses", RegionId = regionEgypt },
                    new Location { Name = "The Japanese University", RegionId = regionEgypt },
                    new Location { Name = "Toshka - Lift Station", RegionId = regionEgypt },
                    new Location { Name = "Toshka - Networks", RegionId = regionEgypt },
                    new Location { Name = "Up Views - 15 Acre", RegionId = regionEgypt },
                    new Location { Name = "Water Treatment Lift Stations 7 & 8", RegionId = regionEgypt },
                    new Location { Name = "Amaala Zone 3", RegionId = regionKSA },
                    new Location { Name = "Amaala Zone 5", RegionId = regionKSA },
                    new Location { Name = "Riyadh Head Office", RegionId = regionKSA }
                );

                await context.SaveChangesAsync();
            }

            if (!context.ScopeOfWorks.Any())
            {
                await context.ScopeOfWorks.AddRangeAsync(
                    new ScopeOfWork { Name = "Doors & Win Schedule" },
                    new ScopeOfWork { Name = "SLABS" }
                );
                await context.SaveChangesAsync();
            }

			if (!context.Employees.Any())
			{
                var id = Guid.NewGuid();
                await context.Employees.AddRangeAsync(
                    new Employee
                    {
                        Id = id,
                        IsAdmin = true , 
                        Password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                        Email = "admin@gmail.com",
                        Name="Admin" , 
                        ReportToId = id ,
                        Role = EmployeeRole.Admin , 
                        Title = "DailyPulseAdmin"
                    }, 
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        IsAdmin = false ,
                        Password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                        Email = "senior@gmail.com",
                        Name = "Senior",
                        ReportToId = id , 
                        Role = EmployeeRole.Senior , 
                        Title = "DailyPulseSenior"
					},
					new Employee
					 {
						 Id = Guid.NewGuid(),
						 IsAdmin = false,
						 Password = BCrypt.Net.BCrypt.HashPassword("123456789"),
						 Email = "teamleader@gmail.com",
						 Name = "TeamLeader",
						 ReportToId = id,
						 Role = EmployeeRole.TeamLeader,
						 Title = "DailyPulseTeamLeader"
					 }
				);

				await context.SaveChangesAsync();
			}

			if (!context.TaskType.Any())
			{
				await context.TaskType.AddRangeAsync(
					new TaskType { Name = "Drawing Generation" },
					new TaskType { Name = "Model" },
					new TaskType { Name = "Coordination" },
					new TaskType { Name = "Other" }
				);

				await context.SaveChangesAsync();
			}

			if (!context.TaskTypeDetails.Any())
			{
				var drawingGeneration = await context.TaskType.Where(x => x.Name == "Drawing Generation")
					.Select(a => a.Id).FirstOrDefaultAsync();

				var model = await context.TaskType.Where(x => x.Name == "Model")
									.Select(a => a.Id).FirstOrDefaultAsync();


				var coordination = await context.TaskType.Where(x => x.Name == "Coordination")
									.Select(a => a.Id).FirstOrDefaultAsync();


				var other = await context.TaskType.Where(x => x.Name == "Other")
									.Select(a => a.Id).FirstOrDefaultAsync();

				await context.TaskTypeDetails.AddRangeAsync(
					new TaskTypeDetails { Name = "Shop Drawing" , TaskTypeId = drawingGeneration },
					new TaskTypeDetails { Name = "As-Built", TaskTypeId = drawingGeneration },

					new TaskTypeDetails { Name = "Modeling", TaskTypeId = model },
					new TaskTypeDetails { Name = "Model Repair", TaskTypeId = model },
					new TaskTypeDetails { Name = "Resolving Clashes", TaskTypeId = model },

					new TaskTypeDetails { Name = "RFIs", TaskTypeId = coordination },
					new TaskTypeDetails { Name = "Meetings", TaskTypeId = coordination },
					new TaskTypeDetails { Name = "Site Visits", TaskTypeId = coordination },
					new TaskTypeDetails { Name = "Project Study", TaskTypeId = coordination }
				);

				await context.SaveChangesAsync();
			}
		}
    }
}
