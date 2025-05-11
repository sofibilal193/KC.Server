using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;

namespace Kashmir.Captain.Server.Application.Users
{
	public interface IUserRepository
	{
		Task<PagedList<User>> GetUsersAsync(int page, int pageSize, string? search, string sortExpression, CancellationToken cancellationToken);
	}
}