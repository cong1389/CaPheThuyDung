using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Attribute;

namespace App.Infra.Data.Mapping
{
    public class AttribureValueConfiguration : EntityTypeConfiguration<AttributeValue>
	{
		public AttribureValueConfiguration()
		{
			ToTable("AttribureValue");

            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            HasRequired(x => x.Attribute)
                .WithMany(x => x.AttributeValues)
                .HasForeignKey(x => x.AttributeId);

            HasMany(x => x.Posts)
                .WithMany(x => x.AttributeValues).Map(x => {
				x.ToTable("PostAttribute");
				x.MapLeftKey("AttibuteValueId");
				x.MapRightKey("PostId");
			});

            
        }
	}
}