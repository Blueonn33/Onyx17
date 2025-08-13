using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class LanguageEntityConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.ImageData).IsRequired();
            builder.Property(l => l.ImageMimeType).IsRequired().HasMaxLength(100);

            builder.HasMany(l => l.Chapters)
                 .WithOne(c => c.Language)
                 .HasForeignKey(c => c.LanguageId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
