using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            // Table name
            builder.ToTable("Users");

            // Primary key
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId).HasColumnName("UserId")
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(u => u.EmailId)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.CityId).HasColumnName("City")
                   .IsRequired();

            builder.Property(u => u.StateId).HasColumnName("State")
                   .IsRequired();

            builder.Property(u => u.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            // BaseSchema mappings
            builder.Property(u => u.CreatedDate)
                   .IsRequired();

            builder.Property(u => u.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(u => u.ModifiedDate)
                   .IsRequired(false);

            builder.Property(u => u.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(u => u.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
