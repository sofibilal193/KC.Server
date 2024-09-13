using System.ComponentModel.DataAnnotations;

namespace Kashmir.Captain.Server.Common.Entities
{
	public abstract class SqlEntity
	{
		[Key]
		public int Id { get; set; }

		public virtual byte[]? Timestamp { get; }

		protected SqlEntity()
		{
		}
	}
}
