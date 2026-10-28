using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class MutualFundsConfiguration : IEntityTypeConfiguration<MutualFunds>
    {
        public void Configure(EntityTypeBuilder<MutualFunds> builder)
        {
            // Table name
            builder.ToTable("MutualFunds");

            // Primary key
            builder.HasKey(mf => mf.MutualFundId);

            builder.Property(mf => mf.MutualFundId)
                   .ValueGeneratedOnAdd();

            // Foreign key relationship
            builder.HasOne(mf => mf.Scheme)
                   .WithMany(s => s.MutualFunds)
                   .HasForeignKey(mf => mf.SchemeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // NAV value precision
            builder.Property(mf => mf.MutualFundValue)
                   .HasColumnType("decimal(18,4)")
                   .IsRequired();

            // NAV date
            builder.Property(mf => mf.MutualFundDate)
                   .HasColumnType("date")
                   .IsRequired();

            // Unique constraint: one NAV per scheme per date
            builder.HasIndex(mf => new { mf.SchemeId, mf.MutualFundDate })
                   .IsUnique()
                   .HasDatabaseName("UQ_SchemeDate");
        }
    }
}
