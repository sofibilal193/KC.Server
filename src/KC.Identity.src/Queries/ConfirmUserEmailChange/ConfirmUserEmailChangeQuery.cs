using MediatR;

namespace KC.KC.Identity
{
	public readonly record struct ConfirmUserEmailChangeQuery(int UserId, string NewEmail, string Token) : IRequest<string>;
}