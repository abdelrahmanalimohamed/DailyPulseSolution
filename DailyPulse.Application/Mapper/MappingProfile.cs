using AutoMapper;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;

namespace DailyPulse.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskWorkLogDTO, TaskWorkLogViewModel>();
            CreateMap<TaskStatusLogs, TaskHistoryViewModel>()
                           .ForMember(dest => dest.DateAndTime, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd-MMM-yy HH:mm")))
                           .ForMember(dest => dest.OldValue, opt => opt.MapFrom(src => src.OldStatus.ToString()))
                           .ForMember(dest => dest.NewValue, opt => opt.MapFrom(src => src.NewStatus.ToString()))
                           .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"Status changed from {src.OldStatus} to {src.NewStatus}"));
        }
    }
}