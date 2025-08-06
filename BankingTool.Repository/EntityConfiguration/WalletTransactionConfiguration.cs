using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            // Table name
            builder.ToTable("WalletTransaction");

            // Primary Key
            builder.HasKey(bt => bt.WalletTransactionId);

            builder.Property(bt => bt.WalletTransactionId)
                   .ValueGeneratedOnAdd();

            // TransactionCategory
            builder.Property(bt => bt.TransactionCategory)
                   .IsRequired()
                   .HasMaxLength(20);

            // Amount
            builder.Property(bt => bt.Amount)
                   .IsRequired();

            // Description
            builder.Property(bt => bt.Description)
                   .HasMaxLength(500);

            // TransactionTime
            builder.Property(bt => bt.TransactionTime)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            // TransactionTag
            builder.Property(bt => bt.TransactionTag)
                   .HasMaxLength(50);

            // BaseSchema properties
            builder.Property(bt => bt.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(bt => bt.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(bt => bt.ModifiedDate)
                   .IsRequired(false);

            builder.Property(bt => bt.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(bt => bt.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
