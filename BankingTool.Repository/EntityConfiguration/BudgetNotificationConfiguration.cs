using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class BudgetNotificationConfiguration : IEntityTypeConfiguration<BudgetNotification>
    {
        public void Configure(EntityTypeBuilder<BudgetNotification> builder)
        {
            builder.ToTable("BudgetNotification");

            // Primary Key
            builder.HasKey(bn => bn.BudgetNotificationId);
            builder.Property(bn => bn.BudgetNotificationId)
                   .ValueGeneratedOnAdd();

            // Foreign Keys
            builder.Property(bn => bn.CustomerId).IsRequired();
            builder.Property(bn => bn.AccountId).IsRequired();
            
            // String properties
            builder.Property(bn => bn.TransactionRole)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(bn => bn.TransactionType)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(bn => bn.TransactionTag)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(bn => bn.TransactionCatagory)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(bn => bn.NotificationStatus)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(bn => bn.Description)
                   .IsRequired()
                   .HasMaxLength(20);

            // Amount
            builder.Property(bn => bn.Amount)
                   .IsRequired()
               .HasColumnType("decimal(18,2)")
               .HasPrecision(18, 2);

            // Dates
            builder.Property(bn => bn.TransactionDate)
                   .IsRequired();

            builder.Property(bn => bn.CreatedDate)
                   .IsRequired();

            builder.Property(bn => bn.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(bn => bn.ModifiedDate)
                   .IsRequired(false);

            builder.Property(bn => bn.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(bn => bn.IsDeleted)
                   .HasDefaultValue(false)
                   .IsRequired();
        }
    }
}
