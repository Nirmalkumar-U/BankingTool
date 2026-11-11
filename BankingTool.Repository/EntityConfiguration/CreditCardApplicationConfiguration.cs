using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardApplicationConfiguration : IEntityTypeConfiguration<CreditCardApplication>
    {
        public void Configure(EntityTypeBuilder<CreditCardApplication> builder)
        {
            // Table name
            builder.ToTable("CreditCardApplication");

            // Primary Key
            builder.HasKey(x => x.ApplicationId);

            builder.Property(x => x.ApplicationId)
                   .ValueGeneratedOnAdd();

            // CustomerId: Required
            builder.Property(x => x.AccountId)
                   .IsRequired();

            // CardType: Required
            builder.Property(x => x.CardType)
                   .IsRequired()
                   .HasMaxLength(50);

            // DesiredLimit: Required
            builder.Property(x => x.DesiredLimit)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // ApprovedLimit: Optional
            builder.Property(x => x.ApprovedLimit)
                   .HasPrecision(18, 2);

            // Status: Enum mapped to string
            builder.Property(x => x.Status)
                   .HasConversion<string>()
                   .IsRequired();

            // ApplicationDate: Required
            builder.Property(x => x.ApplicationDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // ApprovedDate: Optional
            builder.Property(x => x.ApprovedDate);

            // ApprovedBy: Optional
            builder.Property(x => x.ApprovedBy)
                   .HasMaxLength(255);

            // DecisionRemarks: Optional
            builder.Property(x => x.DecisionRemarks);

            // Base Schema Properties
            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => new { x.AccountId, x.Status })
                   .HasDatabaseName("IX_CreditCardApplication_CustomerId_Status");

            builder.HasIndex(x => x.ApplicationDate)
                   .HasDatabaseName("IX_CreditCardApplication_ApplicationDate");
        }
    }
}
