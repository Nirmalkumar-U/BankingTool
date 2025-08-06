using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankingTool.Model.Model;

namespace BankingTool.Repository.EntityConfiguration
{
    public class WalletTransactionDetailConfiguration : IEntityTypeConfiguration<WalletTransactionDetail>
    {
        public void Configure(EntityTypeBuilder<WalletTransactionDetail> builder)
        {
            // Table name
            builder.ToTable("WalletTransactionDetail");

            // Primary Key
            builder.HasKey(btd => btd.WalletTransactionDetailId);

            builder.Property(btd => btd.WalletTransactionDetailId)
                   .ValueGeneratedOnAdd();

            builder.Property(btd => btd.WalletTransactionId)
                   .IsRequired();

            builder.HasOne(btd => btd.WalletTransaction)
                   .WithMany(x => x.WalletTransactionDetail)
                   .HasForeignKey(btd => btd.WalletTransactionId)
                   .HasConstraintName("FK_WalletTransactionDetail_WalletTransactionId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(btd => btd.WalletAccountId)
                   .IsRequired();

            builder.HasOne(btd => btd.WalletAccount)
                   .WithMany(x => x.WalletTransactionDetail)
                   .HasForeignKey(btd => btd.WalletAccountId)
                   .HasConstraintName("FK_WalletTransactionDetail_WalletAccountId")
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
                   .IsRequired();
        }
    }
}
