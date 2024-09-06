using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kashmir.Captain.Server.Data.Configuration
{
	public class ToolEquipmentConfiguration : IEntityTypeConfiguration<Tool>
	{
		public void Configure(EntityTypeBuilder<Tool> builder)
		{
			builder.ToTable("ToolEquipments", GlobalConstants.UtilsSchema);

			builder.HasKey(x => x.Id);

			builder.Property(e => e.Name)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(e => e.Brand)
				.HasMaxLength(10)
				.IsRequired();

			builder.Property(e => e.Description)
				.HasMaxLength(10)
				.IsRequired();
		}
	}
}