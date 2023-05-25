using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class Cart_Configuration : IEntityTypeConfiguration<Cart_Model>
    {
        public void Configure(EntityTypeBuilder<Cart_Model> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.User_Model).WithOne(x => x.Cart_Model)
                .HasForeignKey<Cart_Model>(x => x.UserId);
        }
    }
}
