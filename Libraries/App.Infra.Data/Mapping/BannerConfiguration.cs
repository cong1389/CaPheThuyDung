using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Ads;

namespace App.Infra.Data.Mapping
{
	public class BannerConfiguration : EntityTypeConfiguration<Banner>
	{
		public BannerConfiguration()
		{
			ToTable("Banner");
			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
			HasRequired(x => x.PageBanner).WithMany(x => x.Banners)
                .HasForeignKey(x => x.PageId);
			HasOptional(x => x.MenuLink)
                .WithMany(x => x.Banners)
                .Map(m 
                => m.MapKey("MenuId"));
		}
	}
}