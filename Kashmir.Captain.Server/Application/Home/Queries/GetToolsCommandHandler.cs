using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Infrastructure.Persistance;
using MediatR;
using Kashmir.Captain.Server.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Kashmir.Captain.Server.Application.Home.Queries
{
	public class GetToolsCommandHandler : IRequestHandler<GetToolsCommand, List<ToolDto>>
	{
		private readonly KcDbContext _context;
		private readonly IMapper _mapper;

		public GetToolsCommandHandler(KcDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<ToolDto>> Handle(GetToolsCommand request, CancellationToken cancellationToken)
		{
			var tools = await _context.Tools.ToListAsync(cancellationToken);
			return _mapper.Map<List<ToolDto>>(tools);
		}
	}
}