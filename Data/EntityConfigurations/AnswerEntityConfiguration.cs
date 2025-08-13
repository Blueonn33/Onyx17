using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class AnswerEntityConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Text).IsRequired().HasMaxLength(1000);
            builder.Property(a => a.CreationDate);
            builder.Property(a => a.UserId).IsRequired();
        }
    }
}
