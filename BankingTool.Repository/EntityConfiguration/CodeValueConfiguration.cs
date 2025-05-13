using BankingTool.Model.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CodeValueConfiguration : IEntityTypeConfiguration<CodeValue>
    {
        public void Configure(EntityTypeBuilder<CodeValue> builder)
        {
            // Table Name
            builder.ToTable("CodeValue");

            // Primary Key
            builder.HasKey(cv => cv.CodeValueId);
            builder.Property(cv => cv.CodeValueId)
                   .HasColumnName("CodeValueId")
                   .ValueGeneratedOnAdd();

            // Columns
            builder.Property(cv => cv.TypeName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(cv => cv.TypeCode)
                   .IsRequired();

            builder.Property(cv => cv.CodeValue1)
                   .HasMaxLength(50);

            builder.Property(cv => cv.CodeValue2)
                   .HasMaxLength(50);

            builder.Property(cv => cv.CodeValue3)
                   .HasMaxLength(50);

            builder.Property(cv => cv.CodeValue1Description)
                   .HasMaxLength(100);

            builder.Property(cv => cv.CodeValue2Description)
                   .HasMaxLength(100);

            builder.Property(cv => cv.CodeValue3Description)
                   .HasMaxLength(100);

            builder.Property(cv => cv.InUse)
                   .IsRequired();

            builder.Property(cv => cv.Sequence)
                   .IsRequired(false);
        }
    }
}
