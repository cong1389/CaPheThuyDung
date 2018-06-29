using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.GenericControl;

namespace App.Infra.Data.Mapping
{
    public class GenericControlConfiguration : EntityTypeConfiguration<GenericControl>
    {
        public GenericControlConfiguration()
        {
            ToTable("GenericControl");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            
            HasMany(x => x.MenuLinks)
                 .WithMany(x => x.GenericControls)
                 .Map(x =>
                 {
                     x.ToTable("GenericControlMenuLink");
                     x.MapLeftKey("GenericControlId");
                     x.MapRightKey("MenuLinkId");
                 });
        }
    }
}