using AutoMapper;
using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Application.Extensions;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using System.Data;
using System.Text.RegularExpressions;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IGenericRepository<Project> _repository;
		private readonly IMapper _mapper;
		public CreateProjectHandler(
			IGenericRepository<Project> repository , 
			IMapper mapper)
        {
            _repository = repository;
			_mapper = mapper;
		}
        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
			var normalizedName = request.Name.RemoveWhitespace();

			var existingProject = await _repository.GetFirstOrDefault(
						prj => prj.Name.Trim().ToLower() == normalizedName.ToLower(),
						cancellationToken);

			if (existingProject != null)
			{
				throw new DuplicateNameException("A Project with the same name already exists.");
			}

			//var trade = ParseTrade(request.TradeId);

			
			var status = ParseStatus(request.StatusId);
			var type = ParseProjectType(request.TypeId);

			var project = _mapper.Map<Project>(request);

			//project.Trade = trade;
			project.Status = status;
			project.ProjectType = type;

			foreach (var trade in request.TradeId)
			{
				project.AddTrades(trade);
			}
			await _repository.AddAsync(project, cancellationToken);
        }
		private static Trades ParseTrade(string tradeId)
		{
			var cleanedTrade = Regex.Replace(tradeId, @"\s+", "");

			if (Enum.TryParse(cleanedTrade, true, out Trades trade))
			{
				return trade;
			}
			throw new Exception($"Invalid trade: {tradeId}");
		}
		private static ProjectStatus ParseStatus(string statusId)
		{
			var cleanedStatus = Regex.Replace(statusId, @"\s+", "");

			if (Enum.TryParse(cleanedStatus, true, out ProjectStatus status))
			{
				return status;
			}
			throw new Exception($"Invalid status: {statusId}");
		}
		private static ProjectType ParseProjectType(string typeId)
		{
			var cleanedType = Regex.Replace(typeId, @"\s+", "");

			if (Enum.TryParse(cleanedType, true, out ProjectType status))
			{
				return status;
			}
			throw new Exception($"Invalid type: {typeId}");
		}
	}
}