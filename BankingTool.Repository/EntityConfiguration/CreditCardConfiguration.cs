using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCard");

            builder.HasKey(x => x.CreditCardId);

            builder.Property(x => x.CardType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CardStatus)
                .HasConversion(v => ((int)v).ToString(),    // Convert enum to numeric string for database
                                v => (CreditCardStatus)int.Parse(v))
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.RequestedLimit)
                .HasPrecision(18, 2);

            builder.Property(x => x.ApprovedLimit)
                .HasPrecision(18, 2);

            builder.Property(x => x.CreditLimit)
                .HasPrecision(18, 2);

            builder.Property(x => x.LimitUsed)
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            builder.Property(x => x.AvailableCredit)
                .HasPrecision(18, 2);

            builder.Property(x => x.ApprovedBy)
                .HasMaxLength(255);

            builder.Property(x => x.CardNumber)
                .HasMaxLength(20);

            builder.Property(x => x.CVV)
                .HasMaxLength(4);

            builder.Property(x => x.CardHolderName)
                .HasMaxLength(255);

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ModifiedBy)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ApplicationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(x => x.Account)
                .WithMany(a => a.CreditCard)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
