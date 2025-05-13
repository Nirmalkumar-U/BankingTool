using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class ActionConfiguration : IEntityTypeConfiguration<Model.Action>
    {
        public void Configure(EntityTypeBuilder<Model.Action> builder)
        {
            // Table name
            builder.ToTable("Action");

            // Primary key
            builder.HasKey(a => a.ActionId);

            builder.Property(a => a.ActionId)
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.ActionName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.ActionPath)
                   .HasMaxLength(100);

            builder.Property(a => a.ActionType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Access)
                   .HasMaxLength(15);

            builder.Property(a => a.MenuLevel)
                   .IsRequired(false);

            builder.Property(a => a.ParrentMenuId)
                   .IsRequired(false);

            builder.Property(a => a.Sequence)
                   .IsRequired(false);

            // BaseSchema properties
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
