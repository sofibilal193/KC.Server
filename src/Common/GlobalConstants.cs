using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;

namespace Kashmir.Captain.Server.Common
{
	namespace Kashmir.Captain.Server.Common
	{
		public static class GlobalConstants
		{
			public const string ProjectName = "Kashmir Captain";
			public static readonly List<string> roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Select(v => v.ToString()).ToList();
			public const string IdSchema = "id";
			public const string UtilsSchema = "utils";
			public const int MaxPageSize = 50;
			public const int DefaultPageSize = 10;

			public const string DateFormat = "yyyy-MM-dd";
			public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";


		}
	}
}