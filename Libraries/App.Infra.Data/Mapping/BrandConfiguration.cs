using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Brandes;

namespace App.Infra.Data.Mapping
{
    public class BrandConfiguration : EntityTypeConfiguration<Brand>
    {
        public BrandConfiguration()
        {
            ToTable("Brand");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}