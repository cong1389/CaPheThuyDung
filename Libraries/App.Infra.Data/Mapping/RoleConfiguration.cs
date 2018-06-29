using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Account;

namespace App.Infra.Data.Mapping
{
	public class RoleConfiguration : EntityTypeConfiguration<Role>
	{
		public RoleConfiguration()
		{
			ToTable("Role");
			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("RoleId").HasColumnType("uniqueidentifier").IsRequired();
			HasMany(x => x.Users).WithMany(x => x.Roles).Map(x => {
				x.ToTable("UserRole");
				x.MapLeftKey("RoleId");
				x.MapRightKey("UserId");
			});
		}
	}
}