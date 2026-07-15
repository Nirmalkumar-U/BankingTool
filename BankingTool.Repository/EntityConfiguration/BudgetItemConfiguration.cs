using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class BudgetItemConfiguration : IEntityTypeConfiguration<BudgetItem>
    {
        public void Configure(EntityTypeBuilder<BudgetItem> builder)
        {
            builder.ToTable("BudgetItem");
            builder.HasKey(x => x.BudgetItemId);

            builder.Property(x => x.BudgetItemId)
                .UseIdentityColumn();

            builder.Property(x => x.BudgetPlannerId)
                .IsRequired();

            builder.Property(x => x.BudgetName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.AllocatedAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            // Navigation
            builder.HasOne(x => x.BudgetPlanner)
                .WithMany(x => x.BudgetItems)
                .HasForeignKey(x => x.BudgetPlannerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.BudgetPlannerId);
        }
    }
}
