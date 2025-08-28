using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class QuizResultEntityConfiguration : IEntityTypeConfiguration<QuizResult>
    {
        public void Configure(EntityTypeBuilder<QuizResult> builder)
        {
            builder.ToTable("QuizResults");

            builder.HasKey(qr => qr.Id);
            builder.Property(qr => qr.Score).IsRequired();
            builder.Property(qr => qr.TotalQuestions).IsRequired();
            builder.Property(qr => qr.TakenAt).IsRequired();
            builder.Property(qr => qr.UserId).IsRequired();
            builder.Property(qr => qr.QuizId).IsRequired();

            builder.HasOne(qr => qr.Quiz)
                .WithMany(q => q.QuizResults)
                .HasForeignKey(qr => qr.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(qr => qr.User)
                .WithMany(u => u.QuizResults)
                .HasForeignKey(qr => qr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
