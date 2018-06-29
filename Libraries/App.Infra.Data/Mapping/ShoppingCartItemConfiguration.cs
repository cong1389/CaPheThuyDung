using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Orders;

namespace App.Infra.Data.Mapping
{
    public class ShoppingCartItemConfiguration : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemConfiguration()
        {
            ToTable("ShoppingCartItem");

            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.Customers)
             .WithMany(x => x.ShoppingCartItems)
             .HasForeignKey(x => x.CustomerId);           
        }
    }
}