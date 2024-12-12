using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskHistoryHandler : IRequestHandler<GetTaskHistoryQuery, IEnumerable<TaskHistoryViewModel>>
    {
        private readonly IGenericRepository<TaskHistoryDTO> _repository;
        private readonly IMapper _mapper;
        public GetTaskHistoryHandler(
            IGenericRepository<TaskHistoryDTO> _repository ,
            IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<IEnumerable<TaskHistoryViewModel>> Handle(GetTaskHistoryQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.CallStoredProc("GetTaskHistory",
                new object[] { request.TaskId }, cancellationToken);

            var taskHistoryViewModel = _mapper.Map<IEnumerable<TaskHistoryViewModel>>(results);

            return taskHistoryViewModel;
        }
    }
}