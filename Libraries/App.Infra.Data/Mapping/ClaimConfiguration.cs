using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Account;

namespace App.Infra.Data.Mapping
{
	public class ClaimConfiguration : EntityTypeConfiguration<Claim>
	{
		public ClaimConfiguration()
		{
			ToTable("Claim");
			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("ClaimId").HasColumnType("int")
			    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
			Property(x => x.UserId).HasColumnName("UserId").HasColumnType("uniqueidentifier").IsRequired();
			Property(x => x.ClaimType).HasColumnName("ClaimType").HasColumnType("nvarchar").IsMaxLength().IsOptional();
			Property(x => x.ClaimValue).HasColumnName("ClaimValue").HasColumnType("nvarchar").IsMaxLength().IsOptional();
			HasRequired(x => x.User).WithMany(x => x.Claims).HasForeignKey(x => x.UserId);
		}
	}
}