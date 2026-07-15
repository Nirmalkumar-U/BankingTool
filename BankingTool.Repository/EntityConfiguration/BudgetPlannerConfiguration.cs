using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class BudgetPlannerConfiguration : IEntityTypeConfiguration<BudgetPlanner>
    {
        public void Configure(EntityTypeBuilder<BudgetPlanner> builder)
        {
            builder.ToTable("BudgetPlanner");
            builder.HasKey(x => x.BudgetPlannerId);

            builder.Property(x => x.BudgetPlannerId)
                .UseIdentityColumn();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.PlanName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.TotalBudget)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            // Navigation
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.BudgetItems)
                .WithOne(x => x.BudgetPlanner)
                .HasForeignKey(x => x.BudgetPlannerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.IsDeleted);
        }
    }
}
