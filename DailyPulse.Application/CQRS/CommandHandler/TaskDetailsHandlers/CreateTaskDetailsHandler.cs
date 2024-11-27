﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.TaskDetails;
using DailyPulse.Domain.Entities;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TaskDetailsHandlers
{
    public class CreateTaskDetailsHandler : IRequestHandler<CreateTaskDetailsCommand>
    {
        private readonly IGenericRepository<TaskWorkLog> _repository;

        public CreateTaskDetailsHandler(IGenericRepository<TaskWorkLog> _repository)
        {
            this._repository = _repository;
        }
        public async Task Handle(CreateTaskDetailsCommand request, CancellationToken cancellationToken)
        {
            var taskDetails = new TaskWorkLog
            {
                TaskId = request.TaskId,
                StartTime = request.StartTime,
                PauseTime = request.PauseTime,
                LogDesc = request.LogDesc,
            };

            await _repository.AddAsync(taskDetails, cancellationToken);
        }
    }
}
