using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.KC.Identity
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