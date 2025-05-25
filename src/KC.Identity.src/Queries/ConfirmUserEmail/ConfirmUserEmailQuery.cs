using FluentValidation;
using MediatR;

namespace KC.KC.Identity
{
	public readonly record struct ConfirmUserEmailQuery(string Token, int UserId) : IRequest<string>;

	public class ConfirmUserEmailQueryValidator : AbstractValidator<ConfirmUserEmailQuery>
	{
		public ConfirmUserEmailQueryValidator()
		{
			RuleFor(user => user.Token);
			RuleFor(user => user.UserId)
				.NotEmpty();

		}
	}
}