using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            // Table name
            builder.ToTable("State");

            // Primary key
            builder.HasKey(s => s.StateId);

            builder.Property(s => s.StateId)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.StateName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Abbreviation)
                   .HasMaxLength(10);

            // BaseSchema properties
            builder.Property(s => s.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(s => s.ModifiedDate)
                   .IsRequired(false);

            builder.Property(s => s.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(s => s.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
