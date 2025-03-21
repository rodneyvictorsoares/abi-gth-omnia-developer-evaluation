using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SaleNumber).IsRequired();
            builder.Property(s => s.Customer).IsRequired();
            builder.Property(s => s.Branch).IsRequired();
            builder.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
            builder.HasMany(s => s.Items)
                   .WithOne(i => i.Sale)
                   .HasForeignKey(i => i.SaleId);
        }
    }
}
