using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Data;

namespace App.Infra.Data.Mapping
{
    public class PostsConfiguration : EntityTypeConfiguration<Post>
    {
        public PostsConfiguration()
        {
            ToTable("Post");
            HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            HasRequired(o => o.Manufacturer)
                .WithMany(c => c.Posts)
                .HasForeignKey(o => o.ManufacturerId)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.MenuLink)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.MenuId)
                .WillCascadeOnDelete(true);

            HasMany(x => x.AttributeValues)
                .WithMany(x => x.Posts)
                .Map(x =>
                {
                    x.ToTable("PostAttribute");
                    x.MapLeftKey("PostId");
                    x.MapRightKey("AttibuteValueId");
                });
        }
    }
}