using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    internal class WalletAccountConfiguration : IEntityTypeConfiguration<WalletAccount>
    {
        public void Configure(EntityTypeBuilder<WalletAccount> builder)
        {
            // Table name
            builder.ToTable("WalletAccount");

            // Primary Key
            builder.HasKey(b => b.WalletAccountId);

            builder.Property(b => b.WalletAccountId)
                   .ValueGeneratedOnAdd();

            // Foreign Key: CustomerId → Customer.CustomerId
            builder.Property(b => b.CustomerId)
                   .IsRequired();

            builder.HasOne(b => b.Customer)
                   .WithMany(x => x.WalletAccount)
                   .HasForeignKey(b => b.CustomerId)
                   .HasConstraintName("FK_WalletAccount_CustomerId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.WalletName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.WalletNumber)
                   .IsRequired()
                   .HasMaxLength(8);

            // Balance
            builder.Property(b => b.Balance)
                   .IsRequired()
               .HasColumnType("decimal(18,2)")
               .HasPrecision(18, 2);

            builder.Property(b => b.WalletStatus)
                   .IsRequired()
                   .HasMaxLength(10);

            // BaseSchema Properties
            builder.Property(b => b.CreatedDate)
                   .IsRequired();

            builder.Property(b => b.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(b => b.ModifiedDate)
                   .IsRequired(false);

            builder.Property(b => b.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(b => b.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }

    }
}
