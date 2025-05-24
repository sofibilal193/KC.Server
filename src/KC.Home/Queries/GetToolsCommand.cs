using KC.Application.DTO;
using MediatR;

namespace KC.Application.Home.Queries
{
	public readonly record struct GetToolsQuery() : IRequest<List<ToolDto>>;

}