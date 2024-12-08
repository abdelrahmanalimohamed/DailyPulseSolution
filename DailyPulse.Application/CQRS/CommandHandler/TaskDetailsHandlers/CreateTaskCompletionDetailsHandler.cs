﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.TaskDetails;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.TaskDetailsHandlers
{
    public class CreateTaskCompletionDetailsHandler : IRequestHandler<CreateTaskCompletionDetailsCommand>
    {
        private readonly IGenericRepository<TaskDetail> _repository;

        private readonly IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository;
        public CreateTaskCompletionDetailsHandler(
            IGenericRepository<TaskDetail> _repository ,
            IGenericRepository<DailyPulse.Domain.Entities.Task> _taskRepository)
        {
            this._repository = _repository;
            this._taskRepository = _taskRepository;
        }
        public async Task Handle(CreateTaskCompletionDetailsCommand request, CancellationToken cancellationToken)
        {
            var taskDetails = new TaskDetail
            {
                TaskId = request.TaskId,
                StartTime = request.StartTime,
                PauseTime = request.PauseTime,
                LogDesc = request.LogDesc,
                EndTime = request.EndTime
            };

            await _repository.AddAsync(taskDetails, cancellationToken);

            await UpdateTaskStatusAsync(request.TaskId, cancellationToken);
        }

        private async Task UpdateTaskStatusAsync(Guid taskId, CancellationToken cancellationToken)
        {
            // Retrieve the Task to update its status
            var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }

            task.Status = Status.Completed;

            await _taskRepository.UpdateAsync(task, cancellationToken);
        }
    }
}
