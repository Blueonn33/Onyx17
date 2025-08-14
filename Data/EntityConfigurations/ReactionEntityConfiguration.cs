using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onyx17.Models;

namespace Onyx17.Data.EntityConfigurations
{
    public class ReactionEntityConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.ToTable("Reactions");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Type).IsRequired().HasMaxLength(10);
            builder.Property(r => r.AnswerId).IsRequired();
            builder.Property(r => r.UserId).IsRequired();

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Answer)
                .WithMany(a => a.Reactions)
                .HasForeignKey(r => r.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
