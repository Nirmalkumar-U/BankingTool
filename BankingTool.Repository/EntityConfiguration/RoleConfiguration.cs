using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table name
            builder.ToTable("Role");

            // Primary key
            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleId)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.RoleName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.RoleCode)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(r => r.RoleLevel)
                   .IsRequired();

            // BaseSchema properties
            builder.Property(r => r.CreatedDate)
                   .IsRequired();

            builder.Property(r => r.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(r => r.ModifiedDate)
                   .IsRequired(false);

            builder.Property(r => r.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(r => r.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
