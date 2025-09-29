using DailyPulse.Application.DTO;
using MediatR;

namespace DailyPulse.Application.CQRS.Queries.ProfitCenter;

public class GetProfitCentersQuery : IRequest<IEnumerable<ProfitCenterDTO>>
{
}