using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardTransactionConfiguration : IEntityTypeConfiguration<CreditCardTransaction>
    {
        public void Configure(EntityTypeBuilder<CreditCardTransaction> builder)
        {
            // Table name
            builder.ToTable("CreditCardTransaction");

            // Primary Key
            builder.HasKey(x => x.TransactionId);

            builder.Property(x => x.TransactionId)
                   .ValueGeneratedOnAdd();

            // CreditCardId: Required, Foreign Key
            builder.Property(x => x.CreditCardId)
                   .IsRequired();

            builder.HasOne(x => x.CreditCard)
                   .WithMany(c => c.Transactions)
                   .HasForeignKey(x => x.CreditCardId)
                   .HasConstraintName("FK_CreditCardTransaction_CreditCardId")
                   .OnDelete(DeleteBehavior.Cascade);

            // TransactionDate: Required
            builder.Property(x => x.TransactionDate)
                   .IsRequired();

            // TransactionType: Enum mapped to string
            builder.Property(x => x.TransactionType)
                   .HasConversion<string>()
                   .IsRequired();

            // Amount: Required
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Description: Optional
            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            // MerchantName: Optional
            builder.Property(x => x.MerchantName)
                   .HasMaxLength(255);

            // TransactionStatus: Enum mapped to string
            builder.Property(x => x.TransactionStatus)
                   .HasConversion<string>()
                   .IsRequired();

            // ReferenceNumber: Optional
            builder.Property(x => x.ReferenceNumber)
                   .HasMaxLength(100);

            // Base Schema Properties
            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => new { x.CreditCardId, x.TransactionDate })
                   .HasDatabaseName("IX_CreditCardTransaction_CreditCardId_Date");

            builder.HasIndex(x => x.TransactionType)
                   .HasDatabaseName("IX_CreditCardTransaction_TransactionType");

            builder.HasIndex(x => x.TransactionStatus)
                   .HasDatabaseName("IX_CreditCardTransaction_TransactionStatus");
        }
    }
}
