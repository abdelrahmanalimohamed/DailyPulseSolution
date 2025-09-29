using MediatR;
using System.Collections.Generic;

namespace DailyPulse.Application.CQRS.Commands.Projects
{
    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> TradeId { get; set; }
        public string ProjectNo { get; set; }
        public string StatusId { get; set; }
        public string TypeId { get; set; }
		public Guid LocationId { get; set; }
        public Guid RegionId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
