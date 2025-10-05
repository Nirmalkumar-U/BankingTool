using BankingTool.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.EntityConfiguration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            // Table name
            builder.ToTable("Card");

            // Primary Key
            builder.HasKey(c => c.CardId);

            builder.Property(c => c.CardId)
                   .ValueGeneratedOnAdd();

            // Foreign Key: AccountId
            builder.Property(c => c.AccountId)
                   .IsRequired();

            builder.HasOne(x => x.Account)
                   .WithMany(x => x.Card)
                   .HasForeignKey(c => c.AccountId)
                   .HasConstraintName("FK_Card_AccountId")
                   .OnDelete(DeleteBehavior.Cascade);

            // CardNumber: Nullable
            builder.Property(c => c.CardNumber)
                   .HasMaxLength(500);

            // CardType: Required, Max 15
            builder.Property(c => c.CardType)
                   .IsRequired()
                   .HasMaxLength(15);

            // CardLimit: Required
            builder.Property(c => c.CardLimit)
                   .IsRequired()
               .HasColumnType("decimal(18,2)")
               .HasPrecision(18, 2);

            // ExpireDate: Required
            builder.Property(c => c.ExpireDate)
                   .IsRequired();

            // CVV: Required
            builder.Property(c => c.CVV)
                   .IsRequired();

            // BaseSchema fields
            builder.Property(c => c.CreatedDate)
                   .IsRequired();

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
