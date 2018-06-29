using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Data;

namespace App.Infra.Data.Mapping
{
    public class PostsGalleryConfiguration : EntityTypeConfiguration<PostGallery>
    {
        public PostsGalleryConfiguration()
        {
            ToTable("PostGallery");
            HasKey(x => x.Id)
                .Property(x => x.Id).HasColumnName("Id")
                .HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            //base.HasRequired<Post>((PostGallery x) => x.Post).WithMany((Post x) => x.PostGallerys).HasForeignKey<int>((PostGallery x) => x.PostId).WillCascadeOnDelete(true);
        }
    }
}