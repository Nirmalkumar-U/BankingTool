using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.HasKey(u => u.UserId);  // Primary key
            entity.Property(u => u.Password).IsRequired().HasMaxLength(500);
            entity.Property(u => u.EmailId).IsRequired().HasMaxLength(500);
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.City).IsRequired().HasMaxLength(30);
            entity.Property(u => u.State).IsRequired().HasMaxLength(30);
            entity.Property(u => u.IsActive);
            entity.Property(u => u.CreatedDate).IsRequired();
            entity.Property(u => u.CreatedBy).IsRequired().HasMaxLength(30);
            entity.Property(u => u.ModifiedDate).IsRequired(false);
            entity.Property(u => u.ModifiedBy).IsRequired(false).HasMaxLength(30);
            entity.Property(u => u.IsDeleted);
        }
    }
}
