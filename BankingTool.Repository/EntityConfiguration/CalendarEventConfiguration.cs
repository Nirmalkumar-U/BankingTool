using BankingTool.Model;
using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Recurrence)
                   .HasMaxLength(50);

            builder.Property(e => e.Duration);

            builder.Property(e => e.Priority)
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
