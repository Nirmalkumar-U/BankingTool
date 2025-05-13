using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Table name
            builder.ToTable("UserRole");

            // Composite primary key
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // Foreign key: UserId → Users.UserId
            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserRole)
                   .HasForeignKey(ur => ur.UserId)
                   .HasConstraintName("FK_UserRole_UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign key: RoleId → Role.RoleId
            builder.HasOne(x => x.Role)
                   .WithMany(x => x.UserRole)
                   .HasForeignKey(ur => ur.RoleId)
                   .HasConstraintName("FK_UserRole_RoleId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
