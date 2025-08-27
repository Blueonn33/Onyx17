using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Onyx17.Areas.Identity.Data;
using Onyx17.Data.EntityConfigurations;
using Onyx17.Models;
using System.Reflection.Emit;

namespace Onyx17.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Language> Languages { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new LanguageEntityConfiguration());
        builder.ApplyConfiguration(new ChapterEntityConfiguration());
        builder.ApplyConfiguration(new QuestionEntityConfiguration());
        builder.ApplyConfiguration(new AnswerEntityConfiguration());
        builder.ApplyConfiguration(new ReactionEntityConfiguration());
        builder.ApplyConfiguration(new QuizEntityConfiguration());
        builder.ApplyConfiguration(new QuizQuestionEntityConfiguration());

        builder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
