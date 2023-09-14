using Andretta.Library.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andretta.Library.Database.Migrations.Entities
{
    public class StockEntity : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(s => s.StockId);

            builder.Property(s => s.StockId)
                .ValueGeneratedNever();

            builder.Property(s => s.CountryId)
                .IsRequired();

            builder.Property(s => s.BookId)
                .IsRequired();

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired();

            builder.HasOne(s => s.Book)
                .WithMany(b => b.Stocks)
                .HasForeignKey(s => s.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}