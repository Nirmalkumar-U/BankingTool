using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class RoleAccessConfiguration : IEntityTypeConfiguration<RoleAccess>
    {
        public void Configure(EntityTypeBuilder<RoleAccess> builder)
        {
            // Table name
            builder.ToTable("RoleAccess");

            // Composite Primary Key
            builder.HasKey(ra => new { ra.RoleId, ra.ActionId });

            // Foreign key: RoleId → Role.RoleId
            builder.HasOne(x => x.Role)
                   .WithMany(x => x.RoleAccess)
                   .HasForeignKey(ra => ra.RoleId)
                   .HasConstraintName("FK_RoleAccess_RoleId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign key: ActionId → Action.ActionId
            builder.HasOne(x => x.Action)
                   .WithMany(x => x.RoleAccess)
                   .HasForeignKey(ra => ra.ActionId)
                   .HasConstraintName("FK_RoleAccess_ActionId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
