using MediatR;

namespace DailyPulse.Application.CQRS.Commands.Locations
{
    public class DeleteLocationCommand : IRequest
    {
        public Guid LocationId { get; set; }
    }
}
