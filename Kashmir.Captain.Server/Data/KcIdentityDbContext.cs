using Kashmir.Captain.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kashmir.Captain.Server.Data
{
    public class KcIdentityDbContext : IdentityDbContext<User, Role, int>
    {
        private readonly string _schema;

        public KcIdentityDbContext(DbContextOptions<KcIdentityDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _schema = configuration.GetValue<string>("IdentitySchema") ?? "id";
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(_schema);

            builder.Entity<User>(entity => entity.ToTable(name: "Users", schema: _schema));
            builder.Entity<Role>(entity => entity.ToTable(name: "Roles", schema: _schema));
            builder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles", schema: _schema));
            builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims", schema: _schema));
            builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins", schema: _schema));
            builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims", schema: _schema));
            builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens", schema: _schema));
        }
    }
}
