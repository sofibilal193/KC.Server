using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kashmir.Captain.Server.Infrastructure.Persistance.Configuration
{
	public class MachineConfiguration : IEntityTypeConfiguration<Machine>
	{
		private readonly string _schema;

		public MachineConfiguration(string schema)
		{
			_schema = schema;
		}

		public void Configure(EntityTypeBuilder<Machine> builder)
		{
			builder.ToTable("Machines", _schema);

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
			builder.Property(e => e.Version)
				.HasMaxLength(10)
				.IsRequired();
		}
	}
}