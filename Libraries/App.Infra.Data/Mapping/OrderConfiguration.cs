using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Orders;

namespace App.Infra.Data.Mapping
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Order");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(o => o.BillingAddress)
                 .WithMany()
                 .HasForeignKey(o => o.BillingAddressId)
                 .WillCascadeOnDelete(false);

            HasOptional(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .WillCascadeOnDelete(false);

            HasRequired(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

            Ignore(o => o.OrderStatus);
            Ignore(o => o.PaymentStatus);
            Ignore(o => o.ShippingStatus);
        }
    }
}