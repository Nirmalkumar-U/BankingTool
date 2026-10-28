using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class UserInvestmentConfiguration : IEntityTypeConfiguration<UserInvestment>
    {
        public void Configure(EntityTypeBuilder<UserInvestment> builder)
        {
            // Table name
            builder.ToTable("UserInvestment");

            // Primary key
            builder.HasKey(ui => ui.InvestmentId);

            builder.Property(ui => ui.InvestmentId)
                   .ValueGeneratedOnAdd();

            // UserId (FK to Users table in your system)
            builder.Property(ui => ui.UserId)
                   .IsRequired();

            // Foreign key relationship with Scheme
            builder.HasOne(ui => ui.Scheme)
                   .WithMany(s => s.UserInvestments)
                   .HasForeignKey(ui => ui.SchemeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Installment amount precision
            builder.Property(ui => ui.InstallmentAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Units purchased precision
            builder.Property(ui => ui.UnitsPurchased)
                   .HasColumnType("decimal(18,4)")
                   .IsRequired();

            // Purchase date
            builder.Property(ui => ui.PurchaseDate)
                   .HasColumnType("date")
                   .IsRequired();

            // NAV at purchase precision
            builder.Property(ui => ui.MutualFundAtPurchase)
                   .HasColumnType("decimal(18,4)")
                   .IsRequired();
        }
    }
}
