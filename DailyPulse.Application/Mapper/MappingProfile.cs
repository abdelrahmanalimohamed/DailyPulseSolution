using AutoMapper;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskWorkLogDTO, TaskWorkLogViewModel>();

        CreateMap<TaskHistoryDTO, TaskHistoryViewModel>()
                       .ForMember(dest => dest.OldValue, 
                                            opt => opt.MapFrom(src => src.OldStatus.ToString()))
                       .ForMember(dest => dest.NewValue, 
                                                opt => opt.MapFrom(src => src.NewStatus.ToString()));

        CreateMap<TaskType, TaskTypeDTO>();

        CreateMap<TaskTypeDetails, TaskTypesDetailsDTO>();

        CreateMap<Project, ProjectsDetailsViewModel>()
                    .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.Name))
                    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Name))
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Employee.Name))
                    .ForMember(dest => dest.ProjectNumber, opt => opt.MapFrom(src => src.ProjectNo))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd-MM-yyyy")));

		CreateMap<Task, TaskInnerDetailsViewModel>()
	                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
					.ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Levels.ToString()))
					.ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
					.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmployee.Name))
					.ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom.ToString("dd-MM-yyyy")))
	                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo.ToString("dd-MM-yyyy")))
	                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskDescription))
					.ForMember(dest => dest.EstimatedTime, opt => opt.MapFrom(src => $"{src.EstimatedWorkingHours} Hours"))
					.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TaskTypeDetails.Name));

		CreateMap<Employee, AllEmployeesViewModel>()
	            .ForMember(dest => dest.ReportingTo, opt => opt.MapFrom(src => src.ReportTo.Name))
	            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

		CreateMap<TaskNewRequirements, TaskNewRequirementsViewModel>()
				.ForMember(dest => dest.NewRequirements, opt => opt.MapFrom(src => src.RequirementsDetails))
				.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.EstimatedWorkingHours, opt => opt.MapFrom(src => $"{src.EstimatedWorkingHours} Hours"));

		CreateMap<Employee, SelectedEmployeeDTO>()
		   .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (int)src.Role));
	}
}