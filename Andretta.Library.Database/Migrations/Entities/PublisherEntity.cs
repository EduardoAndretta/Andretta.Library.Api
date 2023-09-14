using Andretta.Library.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andretta.Library.Database.Migrations.Entities
{
    public class PublisherEntity : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publishers");

            builder.HasKey(p => p.PublisherId);

            builder.Property(p => p.PublisherId)
                .ValueGeneratedNever();

            builder.Property(p => p.AddressId)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.Address)
                .WithMany(a => a.Publishers)
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}