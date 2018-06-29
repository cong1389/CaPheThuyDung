using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.GenericControl;

namespace App.Infra.Data.Mapping
{
    public class GenericControlValueConfiguration : EntityTypeConfiguration<GenericControlValue>
    {
        public GenericControlValueConfiguration()
        {
            ToTable("GenericControlValue");

            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.GenericControl)
                           .WithMany(x => x.GenericControlValues)
                           .HasForeignKey(x => x.GenericControlId)
                            .WillCascadeOnDelete(true); ;

        }
    }
}