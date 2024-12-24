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

            builder.Property(s => s.DataOfSale)
                .IsRequired();

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(s => s.Discount)
                .HasColumnType("decimal(18,2)"); 

            builder.Property(s => s.TotalAmount)
                .HasColumnType("decimal(18,2)"); 

            builder.HasMany(s => s.Products)
                .WithOne() 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
