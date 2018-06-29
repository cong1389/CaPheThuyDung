using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Data;

namespace App.Infra.Data.Mapping
{
    public class RepairItemConfiguration : EntityTypeConfiguration<RepairItem>
    {
        public RepairItemConfiguration()
        {
            ToTable("RepairItem");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.Repair)
                .WithMany(x => x.RepairItems)
                .HasForeignKey(x => x.RepairId)
                .WillCascadeOnDelete(true);
        }
    }
}