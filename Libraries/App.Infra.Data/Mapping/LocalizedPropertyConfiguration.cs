using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Domain.Entities.Language;

namespace App.Infra.Data.Mapping
{
    public class LocalizedPropertyConfiguration : EntityTypeConfiguration<LocalizedProperty>
	{
		public LocalizedPropertyConfiguration()
		{
			ToTable("LocalizedProperty");
			HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id").HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            //this.HasRequired(lp => lp.Language)
            // .WithMany()
            // .HasForeignKey(lp => lp.LanguageId);
        }
	}
}