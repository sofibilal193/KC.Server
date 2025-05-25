using Microsoft.AspNetCore.Mvc;
using KC.KC.Identity;
using KC.Application.Common.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace KC.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        #region RegisterUserAsync
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
        #endregion

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Log out User
        /// </summary>
        /// <returns></returns> 
        [HttpPost("logout")]
        public IActionResult LogoutAsync()
        {
            return Ok("Logged out successfully.");
        }
    }
}