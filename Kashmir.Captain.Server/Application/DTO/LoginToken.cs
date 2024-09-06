namespace Kashmir.Captain.Server.Application
{
    public record LoginToken
    {
		public string? Token { get; init; }
		public DateTime? Expiration { get; init; }
    }
}