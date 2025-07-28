using BankingTool.Model;
using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CalendarNoteConfiguration : IEntityTypeConfiguration<CalendarNote>
    {
        public void Configure(EntityTypeBuilder<CalendarNote> builder)
        {
            // Table name
            builder.ToTable("CalendarNote");

            // Primary Key
            builder.HasKey(n => n.CalendarNoteId);

            // Foreign Key
            builder.HasOne<Users>() // Assuming a User entity exists
                   .WithMany()
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(n => n.Message)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(n => n.Date)
                   .IsRequired();

            // BaseSchema properties
            builder.Property(n => n.CreatedDate)
                   .IsRequired();

            builder.Property(n => n.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(n => n.ModifiedDate);

            builder.Property(n => n.ModifiedBy)
                   .HasMaxLength(100);

            builder.Property(n => n.IsDeleted)
                   .IsRequired();
        }
    }
}
