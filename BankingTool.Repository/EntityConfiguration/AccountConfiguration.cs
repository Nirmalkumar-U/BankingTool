using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Table name
            builder.ToTable("Account");

            // Primary Key
            builder.HasKey(a => a.AccountId);

            builder.Property(a => a.AccountId)
                   .ValueGeneratedOnAdd();

            // Foreign Key to Customer
            builder.Property(a => a.CustomerId)
                   .IsRequired();

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Account)
                   .HasForeignKey(a => a.CustomerId)
                   .HasConstraintName("FK_Account_CustomerId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key to Bank
            builder.Property(a => a.BankId)
                   .IsRequired();

            builder.HasOne(x => x.Bank)
                   .WithMany(x => x.Account)
                   .HasForeignKey(a => a.BankId)
                   .HasConstraintName("FK_Account_BankId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Balance
            builder.Property(a => a.Balance)
                   .IsRequired();

            // AccountNumber
            builder.Property(a => a.AccountNumber)
                   .IsRequired()
                   .HasMaxLength(16);

            // AccountTypeId
            builder.Property(a => a.AccountTypeId)
                   .IsRequired();

            // AccountStatus
            builder.Property(a => a.AccountStatus)
                   .IsRequired()
                   .HasMaxLength(15);

            // BaseSchema fields
            builder.Property(a => a.CreatedDate)
                   .IsRequired();

            builder.Property(a => a.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(a => a.ModifiedDate)
                   .IsRequired(false);

            builder.Property(a => a.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(a => a.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
