namespace Kashmir.Captain.Server.Common.Entities
{
	public abstract class BaseEntity
	{
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; } = "Bilal";
		public DateTime? ModifiedDate { get; set; }
		public string ModifiedBy { get; set; } = "Bilal";
		public abstract dynamic GetId();
	}
}
