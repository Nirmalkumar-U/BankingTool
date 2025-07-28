using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CalendarEvent> builder)
        {
            // Table name
            builder.ToTable("CalendarEvent");

            // Primary key
            builder.HasKey(e => e.CalendarEventId);

            // Foreign key to User
            builder.HasOne<Users>() // optional navigation property
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .HasColumnType("nvarchar(max)");

            builder.Property(e => e.StartDateTime)
                   .IsRequired();

            builder.Property(e => e.EndDateTime);

            builder.Property(e => e.EventType)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Recurrence)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(e => e.RecurrenceInterval);

            builder.Property(e => e.Priority)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.IsAllDay)
                   .IsRequired();

            builder.Property(e => e.IsPublic)
                   .IsRequired();

            // BaseSchema fields
            builder.Property(e => e.CreatedDate)
                   .IsRequired();

            builder.Property(e => e.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.ModifiedDate);

            builder.Property(e => e.ModifiedBy)
                   .HasMaxLength(100);

            builder.Property(e => e.IsDeleted)
                   .IsRequired();
        }
    }
}
