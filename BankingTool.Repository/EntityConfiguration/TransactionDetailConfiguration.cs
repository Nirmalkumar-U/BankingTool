using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class TransactionDetailConfiguration : IEntityTypeConfiguration<TransactionDetail>
    {
        public void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            // Table name
            builder.ToTable("TransactionDetail");

            // Primary Key
            builder.HasKey(td => td.TransactionDetailsId);

            builder.Property(td => td.TransactionDetailsId)
                   .ValueGeneratedOnAdd();

            // Foreign Key: TransactionId
            builder.Property(td => td.TransactionId)
                   .IsRequired();

            builder.HasOne(x => x.Transaction)
                   .WithMany(x => x.TransactionDetail)
                   .HasForeignKey(td => td.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key: AccountId
            builder.Property(td => td.AccountId)
                   .IsRequired();

            builder.HasOne(x => x.Account)
                   .WithMany()
                   .HasForeignKey(td => td.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            // TransactionRole: Required, Max Length 10
            builder.Property(td => td.TransactionRole)
                   .IsRequired()
                   .HasMaxLength(10);

            // TransactionType: Required, Max Length 10
            builder.Property(td => td.TransactionType)
                   .IsRequired()
                   .HasMaxLength(10);

            // StageBalance: Required
            builder.Property(td => td.StageBalance)
                   .IsRequired();
        }
    }
}
