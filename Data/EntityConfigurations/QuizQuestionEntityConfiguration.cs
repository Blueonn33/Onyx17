using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class QuizQuestionEntityConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.HasKey(qq => qq.Id);
            builder.Property(qq => qq.QuestionText);
            builder.Property(qq => qq.AnswerA);
            builder.Property(qq => qq.AnswerB);
            builder.Property(qq => qq.AnswerC);
            builder.Property(qq => qq.AnswerD);
            builder.Property(qq => qq.CorrectAnswer).IsRequired().HasMaxLength(1);

            builder.HasOne(qq => qq.Quiz)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
