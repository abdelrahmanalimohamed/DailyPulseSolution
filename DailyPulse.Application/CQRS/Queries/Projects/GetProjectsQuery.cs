﻿using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.Projects
{
    public class GetProjectsQuery : IRequest<IEnumerable<Project>>
    {
    }
}
