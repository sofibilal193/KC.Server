using MediatR;

namespace Kashmir.Captain.Server.Application.Queries
{
     public readonly record struct ConfirmUserEmailChangeQuery(int UserId, string NewEmail, string Token) : IRequest<string>;
}