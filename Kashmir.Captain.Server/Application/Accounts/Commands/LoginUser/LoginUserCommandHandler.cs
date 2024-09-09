using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Kashmir.Captain.Server.Application.DTO;

namespace Kashmir.Captain.Server.Application.Accounts.Commands
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<LoginToken>>
	{
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly IConfiguration _configuration;
		private readonly SignInManager<User> _signInManager;

		public LoginUserCommandHandler(UserManager<User> userManager, IEmailService emailService,
										SignInManager<User> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_emailService = emailService;
			// _urlHelper = urlHelper;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		public async Task<ApiResponse<LoginToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			// Find the user by email
			var user = await _userManager.FindByEmailAsync(request.Email)
				?? throw new NotFoundException(nameof(User), request.Email);

			// Check the user's password
			var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
			if (!passwordCheck)
			{
				return new ApiResponse<LoginToken> { IsSuccess = false, Message = "invalid Credential" };
			}

			// Sign in the user
			var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.RememberMe, lockoutOnFailure: false);
			if (!signInResult.Succeeded)
			{
				if (signInResult.IsLockedOut)
				{
					return new ApiResponse<LoginToken> { IsSuccess = false, Message = "Locked Out" };
				}
				throw new UnauthorizedAccessException();
			}
			var roles = await _userManager.GetRolesAsync(user);

			// Generate JWT token
			var authClaims = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim(ClaimTypes.Role,  (await _userManager.GetRolesAsync(user)).First())
					};

			await _userManager.AddClaimsAsync(user, authClaims);

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				expires: DateTime.Now.AddDays(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);

			// Return the token
			var loginToken = new LoginToken
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = token.ValidTo
			};

			return new ApiResponse<LoginToken> { IsSuccess = true, Message = "Logged In Successfully", Data = loginToken };
		}
	}
}
