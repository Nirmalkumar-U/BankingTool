using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Table name
            builder.ToTable("City");

            // Primary key
            builder.HasKey(c => c.CityId);

            builder.Property(c => c.CityId)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.StateId)
                   .IsRequired();

            builder.Property(c => c.CityName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.PostalCode)
                   .HasMaxLength(15);

            // Foreign key relationship
            builder.HasOne(c => c.State)
                   .WithMany(s => s.City)
                   .HasForeignKey(c => c.StateId)
                   .HasConstraintName("FK_City_StateId")
                   .OnDelete(DeleteBehavior.Restrict);

            // BaseSchema properties
            builder.Property(c => c.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(c => c.ModifiedDate)
                   .IsRequired(false);

            builder.Property(c => c.ModifiedBy)
                   .HasMaxLength(30);

            builder.Property(c => c.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
