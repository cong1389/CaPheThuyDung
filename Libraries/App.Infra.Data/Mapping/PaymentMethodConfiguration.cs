using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Payments;

namespace App.Infra.Data.Mapping
{
    public class PaymentMethodConfiguration : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodConfiguration()
        {
            ToTable("PaymentMethod");
            HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}