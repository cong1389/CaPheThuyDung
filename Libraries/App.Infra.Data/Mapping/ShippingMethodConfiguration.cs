using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Shippings;

namespace App.Infra.Data.Mapping
{
    public class ShippingMethodConfiguration : EntityTypeConfiguration<ShippingMethod>
    {
        public ShippingMethodConfiguration()
        {
            ToTable("ShippingMethod");
            HasKey(x => x.Id)
                .Property(x => x.Id).HasColumnName("Id")
                .HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}