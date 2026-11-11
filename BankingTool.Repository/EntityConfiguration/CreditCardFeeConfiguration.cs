using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardFeeConfiguration : IEntityTypeConfiguration<CreditCardFee>
    {
        public void Configure(EntityTypeBuilder<CreditCardFee> builder)
        {
            // Table name
            builder.ToTable("CreditCardFee");

            // Primary Key
            builder.HasKey(x => x.FeeId);

            builder.Property(x => x.FeeId)
                   .ValueGeneratedOnAdd();

            // CreditCardId: Required, Foreign Key
            builder.Property(x => x.CreditCardId)
                   .IsRequired();

            builder.HasOne(x => x.CreditCard)
                   .WithMany(c => c.Fees)
                   .HasForeignKey(x => x.CreditCardId)
                   .HasConstraintName("FK_CreditCardFee_CreditCardId")
                   .OnDelete(DeleteBehavior.Cascade);

            // FeeType: Required
            builder.Property(x => x.FeeType)
                   .IsRequired()
                   .HasMaxLength(50);

            // Amount: Required
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // EffectiveDate: Required
            builder.Property(x => x.EffectiveDate)
                   .IsRequired();

            // FeeReason: Optional
            builder.Property(x => x.FeeReason)
                   .HasMaxLength(500);

            // Base Schema Properties
            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => new { x.CreditCardId, x.EffectiveDate })
                   .HasDatabaseName("IX_CreditCardFee_CreditCardId_Date");

            builder.HasIndex(x => x.FeeType)
                   .HasDatabaseName("IX_CreditCardFee_FeeType");
        }
    }
}
