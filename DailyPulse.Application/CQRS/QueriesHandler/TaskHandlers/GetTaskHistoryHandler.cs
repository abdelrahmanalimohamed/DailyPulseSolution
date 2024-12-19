using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Tasks;
using DailyPulse.Application.DTO;
using DailyPulse.Application.ViewModel;
using DailyPulse.Domain.Entities;
using Dapper;
using MediatR;
using Task = DailyPulse.Domain.Entities.Task;

namespace DailyPulse.Application.CQRS.QueriesHandler.TaskHandlers
{
    public class GetTaskHistoryHandler : IRequestHandler<GetTaskHistoryQuery, IEnumerable<TaskHistoryViewModel>>
    {
        private readonly IGenericRepository<Task> _repository;
        private readonly IMapper _mapper;
        public GetTaskHistoryHandler(
            IGenericRepository<Task> _repository ,
            IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<IEnumerable<TaskHistoryViewModel>> Handle(GetTaskHistoryQuery request, CancellationToken cancellationToken)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("TaskId" , request.TaskId);

            var results = await _repository.CallStoredProc<TaskHistoryDTO>("GetTaskHistory",
                dynamicParameters, cancellationToken);

            var taskHistoryViewModel = _mapper.Map<IEnumerable<TaskHistoryViewModel>>(results);

            return taskHistoryViewModel;
        }
    }
}