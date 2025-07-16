using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");

            builder.HasKey(n => n.NoteId);

            builder.Property(n => n.UserId)
                .IsRequired();

            builder.Property(n => n.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(n => n.Content)
                .IsRequired();

            builder.Property(n => n.CreatedDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(n => n.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(n => n.ModifiedDate)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(n => n.ModifiedBy)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(n => n.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
