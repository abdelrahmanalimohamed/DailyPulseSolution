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

            CreateMap<TaskHistoryDTO, TaskHistoryViewModel>()
                           .ForMember(dest => dest.OldValue, 
                                                opt => opt.MapFrom(src => src.OldStatus.ToString()))
                           .ForMember(dest => dest.NewValue, 
                                                    opt => opt.MapFrom(src => src.NewStatus.ToString()));

            CreateMap<TaskType, TaskTypeDTO>();
            CreateMap<TaskTypeDetails, TaskTypesDetailsDTO>();
        }
    }
}