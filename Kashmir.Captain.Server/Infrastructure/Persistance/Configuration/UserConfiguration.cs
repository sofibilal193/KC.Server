using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kashmir.Captain.Server.Data.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{

		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users", GlobalConstants.IdSchema);

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

			builder.Property(e => e.Email)
				.IsRequired()
				.HasMaxLength(50);
		}
	}
}