using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.GlobalSetting;

namespace App.Infra.Data.Mapping
{
	public class ContactInformationConfiguration : EntityTypeConfiguration<ContactInformation>
	{
		public ContactInformationConfiguration()
		{
			ToTable("ContactInformation");

            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasOptional(x => x.Province)
                .WithMany(x => x.ContactInformations).HasForeignKey
                (x => x.ProvinceId);

            //base.HasMany<GenericControl>((ContactInformation x) => x.GenericControls)
            //    .WithRequired((GenericControl x) => x.ContactInfo)
            //    .HasForeignKey<int>((GenericControl x) => x.EntityId);

        }
    }
}