using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CreditCardStatementDetailConfiguration : IEntityTypeConfiguration<CreditCardStatementDetail>
    {
        public void Configure(EntityTypeBuilder<CreditCardStatementDetail> builder)
        {
            // Table name
            builder.ToTable("CreditCardStatementDetail");

            // Primary Key
            builder.HasKey(x => x.StatementDetailId);

            builder.Property(x => x.StatementDetailId)
                   .ValueGeneratedOnAdd();

            // StatementId: Required, Foreign Key
            builder.Property(x => x.StatementId)
                   .IsRequired();

            builder.HasOne(x => x.Statement)
                   .WithMany(s => s.StatementDetails)
                   .HasForeignKey(x => x.StatementId)
                   .HasConstraintName("FK_CreditCardStatementDetail_StatementId")
                   .OnDelete(DeleteBehavior.Cascade);

            // TransactionId: Required, Foreign Key
            builder.Property(x => x.TransactionId)
                   .IsRequired();

            builder.HasOne(x => x.Transaction)
                   .WithMany()
                   .HasForeignKey(x => x.TransactionId)
                   .HasConstraintName("FK_CreditCardStatementDetail_TransactionId")
                   .OnDelete(DeleteBehavior.Cascade);

            // TransactionDate: Required
            builder.Property(x => x.TransactionDate)
                   .IsRequired();

            // Description: Optional
            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            // Amount: Required
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // TransactionType: Required
            builder.Property(x => x.TransactionType)
                   .IsRequired()
                   .HasMaxLength(50);

            // Base Schema Properties
            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => x.StatementId)
                   .HasDatabaseName("IX_CreditCardStatementDetail_StatementId");
        }
    }
}
