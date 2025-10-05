using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankingTool.Model.Model;

namespace BankingTool.Repository.EntityConfiguration
{
    public class BudgetTransactionDetailConfiguration : IEntityTypeConfiguration<BudgetTransactionDetail>
    {
        public void Configure(EntityTypeBuilder<BudgetTransactionDetail> builder)
        {
            // Table name
            builder.ToTable("BudgetTransactionDetail");

            // Primary Key
            builder.HasKey(btd => btd.BudgetTransactionDetailId);

            builder.Property(btd => btd.BudgetTransactionDetailId)
                   .ValueGeneratedOnAdd();

            // Foreign Key: BudgetTransactionId
            builder.Property(btd => btd.BudgetTransactionId)
                   .IsRequired();

            builder.HasOne(btd => btd.BudgetTransaction)
                   .WithMany(x => x.BudgetTransactionDetail)
                   .HasForeignKey(btd => btd.BudgetTransactionId)
                   .HasConstraintName("FK_BudgetTransactionDetail_BudgetTransactionId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key: AccountId
            builder.Property(btd => btd.AccountId)
                   .IsRequired();

            builder.HasOne(btd => btd.Account)
                   .WithMany(x => x.BudgetTransactionDetail)
                   .HasForeignKey(btd => btd.AccountId)
                   .HasConstraintName("FK_BudgetTransactionDetail_AccountId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key: BudgetAccountId
            builder.Property(btd => btd.BudgetAccountId)
                   .IsRequired();

            builder.HasOne(btd => btd.BudgetAccount)
                   .WithMany(x => x.BudgetTransactionDetail)
                   .HasForeignKey(btd => btd.BudgetAccountId)
                   .HasConstraintName("FK_BudgetTransactionDetail_BudgetAccountId")
                   .OnDelete(DeleteBehavior.Cascade);

            // TransactionRole
            builder.Property(btd => btd.TransactionRole)
                   .IsRequired()
                   .HasMaxLength(10);

            // TransactionType
            builder.Property(btd => btd.TransactionType)
                   .IsRequired()
                   .HasMaxLength(10);

            // StageBalance
            builder.Property(btd => btd.StageBalance)
                   .IsRequired()
               .HasColumnType("decimal(18,2)")
               .HasPrecision(18, 2);
        }
    }
}
