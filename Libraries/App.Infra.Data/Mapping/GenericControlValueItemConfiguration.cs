using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.GenericControl;

namespace App.Infra.Data.Mapping
{
    public class GenericControlValueItemConfiguration : EntityTypeConfiguration<GenericControlValueItem>
    {
        public GenericControlValueItemConfiguration()
        {
            ToTable("GenericControlValueItem");

            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.GenericControlValue)
                           .WithMany(x => x.GenericControlValueItems)
                           .HasForeignKey(x => x.GenericControlValueId)
                            .WillCascadeOnDelete(true); ;

        }
    }
}