using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Commands
{
    public class ForgotUserPasswordCommand : IRequest<string>
    {
		public string? Email { get; init; }
    }
}