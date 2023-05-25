using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class Role_Configuration : IEntityTypeConfiguration<Role_Model>
    {
        public void Configure(EntityTypeBuilder<Role_Model> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(p => p.Id); // set khóa chính
            builder.Property(p => p.RoleName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(p => p.Status).HasColumnType("int");
        }
    }
}
