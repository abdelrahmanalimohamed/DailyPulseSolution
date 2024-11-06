﻿using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities
{
    public class TaskDetail : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public string LogDesc { get; set; }
    }
}