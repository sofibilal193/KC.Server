using KC.Application.Common.Controllers;
using KC.Application.Users.Queries;
using KC.Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KC.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [Authorize(Policy = nameof(RoleType.User))]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var response = await Mediator.Send(new GetUserQuery(userId));
            return Ok(response);
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUserAsync(int page, int pageSize, string? sort, string? search)
        {
            var response = await Mediator.Send(new GetUsersQuery(page, pageSize, sort, search));
            return Ok(response);
        }
    }
}