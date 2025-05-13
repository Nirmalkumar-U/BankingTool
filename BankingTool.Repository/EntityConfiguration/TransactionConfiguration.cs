using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Model.Transaction>
    {
        public void Configure(EntityTypeBuilder<Model.Transaction> builder)
        {
            // Table name
            builder.ToTable("Transaction");

            // Primary Key
            builder.HasKey(t => t.TransactionId);

            builder.Property(t => t.TransactionId)
                   .ValueGeneratedOnAdd();

            // TransactionCategory: Required, Max Length 20
            builder.Property(t => t.TransactionCategory)
                   .IsRequired()
                   .HasMaxLength(20);

            // Description: Optional, Max Length 500
            builder.Property(t => t.Description)
                   .HasMaxLength(500);

            // Amount: Required
            builder.Property(t => t.Amount)
                   .IsRequired();

            // TransactionTime: Required, Default handled at DB level
            builder.Property(t => t.TransactionTime)
                   .IsRequired();

            // TransactionTag: Optional, Max Length 50
            builder.Property(t => t.TransactionTag)
                   .HasMaxLength(50);

            // BaseSchema fields
            builder.Property(t => t.CreatedDate)
                   .IsRequired();

            builder.Property(t => t.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(t => t.ModifiedDate)
                   .IsRequired(false);

            builder.Property(t => t.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(t => t.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
