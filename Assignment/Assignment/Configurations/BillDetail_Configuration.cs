using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class BillDetail_Configuration : IEntityTypeConfiguration<BillDetail_Model>
    {
        public void Configure(EntityTypeBuilder<BillDetail_Model> builder)
        {
            builder.ToTable("BillDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int").IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,0)").IsRequired();

            builder.HasOne(x => x.Bill_Model).WithMany(x => x.BillDetail_Models)
                .HasForeignKey(x => x.BillId);
            builder.HasOne(x => x.Product_Model).WithMany(x => x.BillDetail_Models)
                .HasForeignKey(x => x.ProductId);

        }
    }
}
