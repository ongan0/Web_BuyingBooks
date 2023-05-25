using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class Category_Configuration : IEntityTypeConfiguration<Category_Model>
    {
        public void Configure(EntityTypeBuilder<Category_Model> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(p => p.Id); // set khóa chính
            builder.Property(p => p.CategoryName).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)").IsRequired();

        }
    }
}
