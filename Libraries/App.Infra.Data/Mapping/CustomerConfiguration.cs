using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Entities.Customers;

namespace App.Infra.Data.Mapping
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasMany(c => c.Addresses)
                .WithMany()
                .Map(m => m.ToTable("CustomerAddresses"));
            HasOptional(c => c.BillingAddress);
            HasOptional(c => c.ShippingAddress);

            //base.HasMany<Address>((Customer x) => x.Addresses)
            //  .WithMany((Address x) => x.Customers)
            //  .Map((ManyToManyAssociationMappingConfiguration x) =>
            //  {
            //      x.ToTable("CustomerAddresses");
            //      x.MapLeftKey(new string[] { "Customer_Id" });
            //      x.MapRightKey(new string[] { "Address_Id" });
            //  });
        }
    }
}