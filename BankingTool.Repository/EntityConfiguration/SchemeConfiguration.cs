using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class SchemeConfiguration : IEntityTypeConfiguration<Scheme>
    {
        public void Configure(EntityTypeBuilder<Scheme> builder)
        {
            // Table name
            builder.ToTable("Scheme");

            // Primary key
            builder.HasKey(s => s.SchemeId);

            // Properties
            builder.Property(s => s.SchemeId)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.SchemeCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(s => s.SchemeCode)
                   .IsUnique();

            builder.Property(s => s.ISINDivPayout)
                   .HasMaxLength(50);

            builder.Property(s => s.ISINGrowth)
                   .HasMaxLength(50);

            builder.Property(s => s.ISINDivReinvestment)
                   .HasMaxLength(50);

            builder.Property(s => s.SchemeName)
                   .IsRequired()
                   .HasMaxLength(200);

            // Relationships
            builder.HasMany(s => s.MutualFunds)
                   .WithOne(mf => mf.Scheme)
                   .HasForeignKey(mf => mf.SchemeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.UserInvestments)
                   .WithOne(ui => ui.Scheme)
                   .HasForeignKey(ui => ui.SchemeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}