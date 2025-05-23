﻿using DailyPulse.Domain.Base;

namespace DailyPulse.Domain.Entities;
public class TaskNewRequirements : BaseEntity
{
    public Guid TaskId { get; set; }
    public Task Task { get; set; }
    public Guid? CreatedBy { get; set; }
    public Employee Employee { get; set; }
    public string RequirementsDetails { get; set; }
	public string? EstimatedWorkingHours { get; set; }
}