using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class CartDetail_Configuration : IEntityTypeConfiguration<CartDetail_Model>
    {
        public void Configure(EntityTypeBuilder<CartDetail_Model> builder)
        {
            builder.ToTable("CartDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Quantity).HasColumnType("int").IsRequired();

            builder.HasOne(x=>x.Cart_Model).WithMany(x=>x.CartDetail_Models)
                .HasForeignKey(x=>x.UserId);
            builder.HasOne(x=>x.Product_Model).WithMany(x=>x.CartDetail_Models)
                .HasForeignKey(x=>x.ProductId);
        }
    }
}
