using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                   .IsRequired();

            builder.HasOne(c => c.User)
                   .WithMany() 
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.Property(c => c.Date)
                   .IsRequired();

            builder.HasMany(c => c.CartProducts)
                   .WithOne() 
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
