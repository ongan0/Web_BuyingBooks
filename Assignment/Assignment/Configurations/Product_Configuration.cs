using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class Product_Configuration : IEntityTypeConfiguration<Product_Model>
    {
        public void Configure(EntityTypeBuilder<Product_Model> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Price).HasColumnType("dec").IsRequired();
            builder.Property(x => x.AvailableQuantity).HasColumnType("int").IsRequired();
            builder.Property(x => x.LinkImg).HasColumnType("varchar(1000)").IsRequired();
            builder.Property(x => x.Supplier).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.Status).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(x => x.Author).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(x => x.Publisher).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(x => x.YearOfPublication).HasColumnType("int").IsRequired();
            builder.Property(x => x.Weight).HasColumnType("int").IsRequired();
            builder.Property(x => x.Size).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Page).HasColumnType("int").IsRequired();


            builder.HasOne(x=>x.Category_Model).WithMany(x=>x.Product_Models)
                .HasForeignKey(x=>x.CateId);
        }
    }
}
