using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kashmir.Captain.Server.Data.Configuration
{
	public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
	{
		public void Configure(EntityTypeBuilder<Worker> builder)
		{
			builder.ToTable("Workers", GlobalConstants.UtilsSchema);

			builder.HasKey(x => x.Id);

			builder.Property(e => e.FirstName)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(e => e.LastName)
				.HasMaxLength(10)
				.IsRequired();

			builder.Property(e => e.PhoneNumber)
				.HasMaxLength(10)
				.IsRequired();
		}
	}
}