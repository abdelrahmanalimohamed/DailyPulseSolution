using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Regions;
using DailyPulse.Application.DTO;
using DailyPulse.Domain.Entities;
using MediatR;

namespace DailyPulse.Application.CQRS.QueriesHandler.RegionHandlers
{
    public class GetRegionsHandler : IRequestHandler<GetRegionsQuery, IEnumerable<RegionsDTO>>
    {
        private readonly IGenericRepository<Region> _repository;

        public GetRegionsHandler(IGenericRepository<Region> _repository)
        {
           this._repository = _repository;
        }

        public async Task<IEnumerable<RegionsDTO>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions =  await _repository.GetAllAsync(cancellationToken);

            var regionsDTOs = regions.Select(region => new RegionsDTO
            {
                Id = region.Id,
                Name = region.Name
            });

            return regionsDTOs;
        }
    }
}
