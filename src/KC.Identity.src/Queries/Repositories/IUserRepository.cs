using KC.Common.Extensions;
using KC.Infrastructure.Persistance.Entities;

namespace KC.Application.Users
{
	public interface IUserRepository
	{
		Task<PagedList<User>> GetUsersAsync(int page, int pageSize, string? search, string sortExpression, CancellationToken cancellationToken);
	}
}