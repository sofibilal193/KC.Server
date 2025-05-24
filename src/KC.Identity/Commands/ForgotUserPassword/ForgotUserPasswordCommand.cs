using MediatR;

namespace KC.KC.Identity
{
	public class ForgotUserPasswordCommand : IRequest<string>
	{
		public string Email { get; init; } = "";
	}
}