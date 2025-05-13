using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditScoreConfiguration : IEntityTypeConfiguration<CreditScore>
    {
        public void Configure(EntityTypeBuilder<CreditScore> builder)
        {
            // Table name
            builder.ToTable("CreditScore");

            // Primary Key
            builder.HasKey(cs => cs.CreditScoreId);

            builder.Property(cs => cs.CreditScoreId)
                   .ValueGeneratedOnAdd();

            // Foreign Key to Customer
            builder.Property(cs => cs.CustomerId)
                   .IsRequired();

            builder.HasOne(cs => cs.Customer)
                    .WithOne(c => c.CreditScore)
                    .HasForeignKey<CreditScore>(cs => cs.CustomerId)
                    .HasConstraintName("FK_CreditScore_CustomerId")
                    .OnDelete(DeleteBehavior.Cascade);


            // CreditScoreValue
            builder.Property(cs => cs.CreditScoreValue)
                   .IsRequired(false);

            // Status
            builder.Property(cs => cs.Status)
                   .IsRequired()
                   .HasMaxLength(15);

            // Description
            builder.Property(cs => cs.Description)
                   .HasMaxLength(500);

            // BaseSchema properties
            builder.Property(cs => cs.CreatedDate)
                   .IsRequired();

            builder.Property(cs => cs.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(cs => cs.ModifiedDate)
                   .IsRequired(false);

            builder.Property(cs => cs.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(cs => cs.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
