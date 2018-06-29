using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Menu;

namespace App.Infra.Data.Mapping
{
	public class MenuLinkConfiguration : EntityTypeConfiguration<MenuLink>
	{
		public MenuLinkConfiguration()
		{
			ToTable("MenuLink");
			HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

			HasOptional(e => e.ParentMenu)
                .WithMany().HasForeignKey(m => m.ParentId);

            HasMany(x => x.GenericControls)
              .WithMany(x => x.MenuLinks)
              .Map(x => {
                  x.ToTable("GenericControlMenuLink");
                  x.MapLeftKey("MenuLinkId");
                  x.MapRightKey("GenericControlId");
              });

        }
	}
}