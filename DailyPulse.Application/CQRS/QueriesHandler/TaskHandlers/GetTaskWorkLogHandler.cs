using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using Dapper;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskWorkLogHandler : IRequestHandler<GetTaskWorkLogQuery, IEnumerable<TaskWorkLogViewModel>>
    {
        public readonly IGenericRepository<Task> _repository;

        private readonly IMapper _mapper;
        public GetTaskWorkLogHandler(
            IGenericRepository<Task> _repository , 
            IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }

        public async Task<IEnumerable<TaskWorkLogViewModel>> Handle(GetTaskWorkLogQuery request, CancellationToken cancellationToken)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("TaskId", request.TaskId);

            var results = await _repository.CallStoredProc<TaskWorkLogDTO>("GetTaskWorkLogs",
                dynamicParameters, cancellationToken);

            return _mapper.Map<IEnumerable<TaskWorkLogViewModel>>(results);
        }
    }
}