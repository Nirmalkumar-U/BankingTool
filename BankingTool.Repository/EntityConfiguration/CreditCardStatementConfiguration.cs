using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardStatementConfiguration : IEntityTypeConfiguration<CreditCardStatement>
    {
        public void Configure(EntityTypeBuilder<CreditCardStatement> builder)
        {
            // Table name
            builder.ToTable("CreditCardStatement");

            // Primary Key
            builder.HasKey(x => x.StatementId);

            builder.Property(x => x.StatementId)
                   .ValueGeneratedOnAdd();

            // CreditCardId: Required, Foreign Key
            builder.Property(x => x.CreditCardId)
                   .IsRequired();

            builder.HasOne(x => x.CreditCard)
                   .WithMany(c => c.Statements)
                   .HasForeignKey(x => x.CreditCardId)
                   .HasConstraintName("FK_CreditCardStatement_CreditCardId")
                   .OnDelete(DeleteBehavior.Cascade);

            // BillingCycleStartDate: Required
            builder.Property(x => x.BillingCycleStartDate)
                   .IsRequired();

            // BillingCycleEndDate: Required
            builder.Property(x => x.BillingCycleEndDate)
                   .IsRequired();

            // StatementDate: Required
            builder.Property(x => x.StatementDate)
                   .IsRequired();

            // TotalCharges: Required
            builder.Property(x => x.TotalCharges)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // TotalPayments: Required
            builder.Property(x => x.TotalPayments)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Interest: Required
            builder.Property(x => x.Interest)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0);

            // Fees: Required
            builder.Property(x => x.Fees)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0);

            // PreviousBalance: Required
            builder.Property(x => x.PreviousBalance)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // CardBalance: Required
            builder.Property(x => x.CardBalance)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // MinimumDueAmount: Required
            builder.Property(x => x.MinimumDueAmount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // DueDate: Required
            builder.Property(x => x.DueDate)
                   .IsRequired();

            // StatementStatus: Enum mapped to string
            builder.Property(x => x.StatementStatus)
                   .HasConversion<string>()
                   .IsRequired();

            // Base Schema Properties
            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => new { x.CreditCardId, x.StatementDate })
                   .HasDatabaseName("IX_CreditCardStatement_CreditCardId_StatementDate");

            builder.HasIndex(x => x.DueDate)
                   .HasDatabaseName("IX_CreditCardStatement_DueDate");

            builder.HasIndex(x => x.StatementStatus)
                   .HasDatabaseName("IX_CreditCardStatement_StatementStatus");
        }
    }
}
