using AutoMapper;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;

namespace DailyPulse.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskWorkLogDTO, TaskWorkLogViewModel>();
        }
    }
}