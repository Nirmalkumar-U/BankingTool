using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // Table name
            builder.ToTable("Staff");

            // Primary key
            builder.HasKey(s => s.StaffId);

            builder.Property(s => s.StaffId)
                   .ValueGeneratedOnAdd();

            // Required foreign key
            builder.Property(s => s.UserId)
                   .IsRequired();

            builder.HasOne(s => s.User)
               .WithOne(u => u.Staff)
               .HasForeignKey<Staff>(s => s.UserId)
               .HasConstraintName("FK_Staff_UserId")
               .OnDelete(DeleteBehavior.Cascade);

            // Other properties
            builder.Property(s => s.StaffLevel)
                   .IsRequired();

            // BaseSchema properties
            builder.Property(s => s.CreatedDate)
                   .IsRequired();

            builder.Property(s => s.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(s => s.ModifiedDate)
                   .IsRequired(false);

            builder.Property(s => s.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(s => s.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
