using Kashmir.Captain.Server.Application.DTO;
using MediatR;

namespace Kashmir.Captain.Server.Application.Home.Queries
{
	public readonly record struct GetToolsCommand(int Id) : IRequest<List<ToolDto>>;

}