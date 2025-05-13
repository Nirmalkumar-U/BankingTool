using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingTool.Repository.EntityConfiguration
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            // Table name
            builder.ToTable("Bank");

            // Primary Key
            builder.HasKey(b => b.BankId);

            builder.Property(b => b.BankId)
                   .ValueGeneratedOnAdd();

            // BankName: Required, max length 100
            builder.Property(b => b.BankName)
                   .IsRequired()
                   .HasMaxLength(100);

            // BankAbbrivation: Required, max length 10
            builder.Property(b => b.BankAbbrivation)
                   .IsRequired()
                   .HasMaxLength(10);
        }
    }
}
