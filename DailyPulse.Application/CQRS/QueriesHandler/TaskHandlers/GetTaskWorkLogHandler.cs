using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskWorkLogHandler : IRequestHandler<GetTaskWorkLogQuery, IEnumerable<TaskWorkLogViewModel>>
    {
        public readonly IGenericRepository<TaskWorkLogDTO> _repository;

        private readonly IMapper _mapper;
        public GetTaskWorkLogHandler(
            IGenericRepository<TaskWorkLogDTO> _repository , 
            IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }

        public async Task<IEnumerable<TaskWorkLogViewModel>> Handle(GetTaskWorkLogQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.CallStoredProc("GetTaskWorkLogs", 
                new object[] { request.TaskId }, cancellationToken);

            return _mapper.Map<IEnumerable<TaskWorkLogViewModel>>(results);
        }
    }
}
