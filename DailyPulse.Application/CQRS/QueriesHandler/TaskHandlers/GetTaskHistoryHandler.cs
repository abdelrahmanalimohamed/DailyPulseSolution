using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskHistoryHandler : IRequestHandler<GetTaskHistoryQuery, IEnumerable<TaskHistoryViewModel>>
    {
        private readonly IGenericRepository<TaskStatusLogs> _repository;
        private readonly IMapper _mapper;
        public GetTaskHistoryHandler(
            IGenericRepository<TaskStatusLogs> _repository ,
            IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<IEnumerable<TaskHistoryViewModel>> Handle(GetTaskHistoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.FindAsync(x => x.TaskId == request.TaskId , cancellationToken);

            var taskHistoryViewModel = _mapper.Map<IEnumerable<TaskHistoryViewModel>>(result);

            return taskHistoryViewModel;
        }
    }
}