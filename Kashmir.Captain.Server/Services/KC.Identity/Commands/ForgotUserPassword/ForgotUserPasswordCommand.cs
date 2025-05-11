using MediatR;

namespace Kashmir.Captain.Server.KC.Identity
{
	public class ForgotUserPasswordCommand : IRequest<string>
	{
		public string Email { get; init; } = "";
	}
}