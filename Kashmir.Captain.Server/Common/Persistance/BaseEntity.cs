namespace Kashmir.Captain.Server.Common.Persistance
{
	public abstract class BaseEntity
	{
		virtual public DateTime? CreatedDate { get; set; }
		virtual public string? CreatedBy { get; set; }
		virtual public DateTime? ModifiedDate { get; set; }
		virtual public string? ModifiedBy { get; set; }
	}
}
