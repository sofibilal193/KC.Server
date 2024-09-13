using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kashmir.Captain.Server.Application.Workers.Commands
{
	public class UpsertToolCommandHandler : IRequestHandler<UpsertToolCommand, int>
	{
		private readonly KcDbContext _contex;
		public UpsertToolCommandHandler(KcDbContext contex)
		{
			_contex = contex;
		}

		public async Task<int> Handle(UpsertToolCommand request, CancellationToken cancellationToken)
		{
			Tool? tool = null;
			if (request.Id.HasValue)
			{
				tool = await _contex.Tools.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Tool), request.Id);
				_contex.Update(tool);
			}
			else
			{
				tool = new Tool(request.Name, request.Brand, request.Description);
				await _contex.AddAsync(tool, cancellationToken);
			}

			await _contex.SaveChangesAsync(cancellationToken);

			return tool.Id;
		}
	}
}