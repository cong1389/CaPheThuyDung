using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Data;

namespace App.Infra.Data.Mapping
{
	public class GalleryImageConfiguration : EntityTypeConfiguration<GalleryImage>
	{
		public GalleryImageConfiguration()
		{
			ToTable("GalleryImage");
			HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.Post)
                .WithMany(x => x.GalleryImages)
                .HasForeignKey(x => x.PostId)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.AttributeValue)
                .WithMany(x => x.GalleryImages)
                .HasForeignKey(x => x.AttributeValueId)
                .WillCascadeOnDelete(true);
		}
	}
}