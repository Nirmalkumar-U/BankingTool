using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Table name
            builder.ToTable("Customer");

            // Primary Key
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId)
                   .ValueGeneratedOnAdd();

            // Foreign Key to Users
            builder.Property(c => c.UserId).HasColumnName("UserId")
                   .IsRequired();

            builder.HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .HasConstraintName("FK_Customer_UserId")
                    .OnDelete(DeleteBehavior.Cascade);

            // CustomerLevel
            builder.Property(c => c.CustomerLevel)
                   .IsRequired();

            // PrimaryAccountNumber
            builder.Property(c => c.PrimaryAccountNumber)
                   .HasMaxLength(16);

            // BaseSchema properties
            builder.Property(c => c.CreatedDate)
                   .IsRequired();

            builder.Property(c => c.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(c => c.ModifiedDate)
                   .IsRequired(false);

            builder.Property(c => c.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(c => c.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
