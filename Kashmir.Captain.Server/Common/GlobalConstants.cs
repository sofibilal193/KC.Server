using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;

namespace Kashmir.Captain.Server.Common
{
	namespace Kashmir.Captain.Server.Common
	{
		public static class GlobalConstants
		{
			public const string ProjectName = "Kashmir Captain";

			public static readonly List<string> roleNames = new List<string> { nameof(RoleType.SuperAdmin), nameof(RoleType.Admin), nameof(RoleType.User) };
			public const string IdSchema = "id";
			public const string UtilsSchema = "utils";
			public const int MaxPageSize = 50;
			public const int DefaultPageSize = 10;

			public const string DateFormat = "yyyy-MM-dd";
			public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

			public static string GetEmailRegistrationBody(string? firstName, string? lastName, string? link, string end = "")
			{
				return $@"<h2>Welcome {firstName} {lastName} to {ProjectName} .</h2>
							<p><a href='{link}'>Click here</a> to confirm your email.</p>
							<p>If you did not register for this account, please ignore this email.</p>
							<h3>Thank You For Registration.</h3>";
			}
		}
	}
}