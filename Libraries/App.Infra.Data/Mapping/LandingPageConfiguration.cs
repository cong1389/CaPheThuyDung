using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Other;

namespace App.Infra.Data.Mapping
{
	public class LandingPageConfiguration : EntityTypeConfiguration<LandingPage>
	{
		public LandingPageConfiguration()
		{
			ToTable("LandingPage");
			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.ContactInformation)
                .WithMany(x => x.LandingPages)
                .HasForeignKey(x => x.ShopId);
		}
	}
}