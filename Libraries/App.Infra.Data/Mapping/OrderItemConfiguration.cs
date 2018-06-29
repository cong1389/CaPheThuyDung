using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Orders;

namespace App.Infra.Data.Mapping
{
    public class OrderItemConfiguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfiguration()
        {
            ToTable("OrderItem");
            HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(orderItem => orderItem.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(orderItem => orderItem.OrderId);

            //HasRequired(orderItem => orderItem.Post)
            //    .WithMany()
            //    .HasForeignKey(orderItem => orderItem.PostId);

            HasRequired(orderItem => orderItem.Post)
                .WithMany()
                .HasForeignKey(orderItem => orderItem.PostId)
                .WillCascadeOnDelete(true);
        }
    }
}