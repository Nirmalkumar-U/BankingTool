using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardStatementConfiguration : IEntityTypeConfiguration<CreditCardStatement>
    {
        public void Configure(EntityTypeBuilder<CreditCardStatement> builder)
        {
            builder.ToTable("CreditCardStatement");

            // Primary Key
            builder.HasKey(x => x.StatementId);

            builder.Property(x => x.StatementId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.CreditCardId)
                   .IsRequired();

            builder.HasOne(x => x.CreditCard)
                   .WithMany(c => c.Statements)
                   .HasForeignKey(x => x.CreditCardId)
                   .HasConstraintName("FK_CreditCardStatement_CreditCardId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.BillingCycleStartDate)
                   .IsRequired();

            builder.Property(x => x.BillingCycleEndDate)
                   .IsRequired();

            builder.Property(x => x.StatementDate)
                   .IsRequired();

            builder.Property(x => x.TotalPayments)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.Fees)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(x => x.StatementStatus)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
