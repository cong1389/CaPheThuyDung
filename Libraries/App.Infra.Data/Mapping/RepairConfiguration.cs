using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Data;

namespace App.Infra.Data.Mapping
{
    public class RepairConfiguration : EntityTypeConfiguration<Repair>
    {
        public RepairConfiguration()
        {
            ToTable("Repair");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.Brand)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.BrandId)
                .WillCascadeOnDelete(true);
        }
    }
}