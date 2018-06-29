using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Account;

namespace App.Infra.Data.Mapping
{
	public class UserConfiguration : EntityTypeConfiguration<User>
	{
		public UserConfiguration()
		{
			ToTable("User");
			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("UserId").HasColumnType("uniqueidentifier").IsRequired();
			Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasColumnType("nvarchar").IsMaxLength().IsOptional();
			Property(x => x.SecurityStamp).HasColumnName("SecurityStamp").HasColumnType("nvarchar").IsMaxLength().IsOptional();
			Property(x => x.UserName).HasColumnName("UserName").HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
			Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
			HasMany(x => x.Roles).WithMany(x => x.Users).Map(x => {
				x.ToTable("UserRole");
				x.MapLeftKey("UserId");
				x.MapRightKey("RoleId");
			});
			HasMany(x => x.Claims).WithRequired(x => x.User).HasForeignKey(x => x.UserId);

            HasMany(x => x.Logins)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);
		}
	}
}