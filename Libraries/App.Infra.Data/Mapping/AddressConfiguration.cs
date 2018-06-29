using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Common;

namespace App.Infra.Data.Mapping
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
	{
		public AddressConfiguration()
		{
			ToTable("Address");

			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int")
			    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            //base.HasMany<Customer>((Address x) => x.Customers)
            //  .WithMany((Customer x) => x.Addresses)
            //  .Map((ManyToManyAssociationMappingConfiguration x) =>
            //  {
            //      x.ToTable("CustomerAddresses");
            //      x.MapLeftKey(new string[] { "Address_Id" });
            //      x.MapRightKey(new string[] { "Customer_Id" });
            //  });

        }
	}
}