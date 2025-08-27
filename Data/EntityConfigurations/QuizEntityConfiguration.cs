using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class QuizEntityConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired().HasMaxLength(100);
            builder.Property(q => q.Description).HasMaxLength(1000);
            builder.Property(q => q.ImageData).IsRequired();
            builder.Property(q => q.ImageMimeType).IsRequired().HasMaxLength(100);

            builder.HasMany(q => q.QuizQuestions)
                .WithOne(qq => qq.Quiz)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
