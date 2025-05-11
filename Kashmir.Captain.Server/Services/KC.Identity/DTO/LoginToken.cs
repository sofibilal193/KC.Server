namespace Kashmir.Captain.Server.Application.DTO
{
	public record LoginToken
	{
		public string? Token { get; init; }
		public DateTime? Expiration { get; init; }
	}
}