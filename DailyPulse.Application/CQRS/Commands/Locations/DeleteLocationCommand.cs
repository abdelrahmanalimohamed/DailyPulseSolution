using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Locations
{
    public class DeleteLocationCommand : IRequest<Unit>
    {
        public Guid LocationId { get; set; }
    }
}
