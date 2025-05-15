using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    internal class BudgetAccountConfiguration : IEntityTypeConfiguration<BudgetAccount>
    {
        public void Configure(EntityTypeBuilder<BudgetAccount> builder)
        {
            // Table name
            builder.ToTable("BudgetAccount");

            // Primary Key
            builder.HasKey(b => b.BudgetAccountId);

            builder.Property(b => b.BudgetAccountId)
                   .ValueGeneratedOnAdd();

            // Foreign Key: CustomerId → Customer.CustomerId
            builder.Property(b => b.CustomerId)
                   .IsRequired();

            builder.HasOne(b => b.Customer)
                   .WithMany(x => x.BudgetAccount)
                   .HasForeignKey(b => b.CustomerId)
                   .HasConstraintName("FK_BudgetAccount_CustomerId")
                   .OnDelete(DeleteBehavior.Cascade);

            // BudgetName
            builder.Property(b => b.BudgetName)
                   .IsRequired()
                   .HasMaxLength(50);

            // BudgetNumber
            builder.Property(b => b.BudgetNumber)
                   .IsRequired()
                   .HasMaxLength(8);

            // Balance
            builder.Property(b => b.Balance)
                   .IsRequired();

            // BudgetStatus
            builder.Property(b => b.BudgetStatus)
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
