using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            // Table name
            builder.ToTable("CreditCard");

            // Primary Key
            builder.HasKey(x => x.CreditCardId);

            builder.Property(x => x.CreditCardId)
                   .ValueGeneratedOnAdd();

            // Custom Id
            builder.Property(x => x.AccountId)
                   .IsRequired();

            // CardNumber: Encrypted, Required
            builder.Property(x => x.CardNumber)
                   .IsRequired();

            // CVV: Encrypted, Required
            builder.Property(x => x.CVV)
                   .IsRequired();

            // CardHolderName: Required
            builder.Property(x => x.CardHolderName)
                   .IsRequired()
                   .HasMaxLength(255);

            // CardType: Required
            builder.Property(x => x.CardType)
                   .IsRequired()
                   .HasMaxLength(50);

            // Status: Enum mapped to string
            builder.Property(x => x.Status)
                   .HasConversion<string>();

            // IssueDate: Required
            builder.Property(x => x.IssueDate)
                   .IsRequired();

            // ExpiryDate: Required
            builder.Property(x => x.ExpiryDate)
                   .IsRequired();

            // CreditLimit: Required
            builder.Property(x => x.CreditLimit)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // CurrentBalance: Required
            builder.Property(x => x.CurrentBalance)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0);

            // AvailableCredit: Required
            builder.Property(x => x.AvailableCredit)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Base Schema Properties
            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => new { x.AccountId, x.Status })
                   .HasDatabaseName("IX_CreditCard_CustomerId_Status");

            builder.HasIndex(x => x.CardNumber)
                   .HasDatabaseName("IX_CreditCard_CardNumber");
        }
    }
}
