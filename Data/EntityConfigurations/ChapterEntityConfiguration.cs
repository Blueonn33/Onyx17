using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class ChapterEntityConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("Chapters");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(700);
            builder.Property(c => c.PdfFileData).IsRequired();

            builder.HasOne(c => c.Language)
                 .WithMany(l => l.Chapters)
                 .HasForeignKey(c => c.LanguageId);
        }
    }
}
