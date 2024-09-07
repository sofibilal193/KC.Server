using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using MediatR;

namespace Kashmir.Captain.Server.Application.Users.Queries
{
	/// <summary>
	/// Get All Users
	/// </summary>
	/// <param name="Page">Page Number</param>
	/// <param name="PageSize">Page Size</param>
	/// <param name="Sort">Sort on Email or First Name</param>
	/// <param name="search"></param> <summary>
	/// 
	/// </summary>
	public readonly record struct GetUsersQuery(int Page, int PageSize, string? Sort, string? search) : IRequest<PagedList<UserDto>>;
}