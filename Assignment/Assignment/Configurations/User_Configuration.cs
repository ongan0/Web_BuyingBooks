using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class User_Configuration : IEntityTypeConfiguration<User_Model>
    {
        public void Configure(EntityTypeBuilder<User_Model> builder)
        {
            builder.ToTable("User");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserName).IsRequired().HasColumnType("nvarchar(30)");
            builder.Property(p => p.Password).IsRequired().HasColumnType("varchar(30)");
            builder.Property(p => p.FullName).HasColumnType("nvarchar(300)");
            builder.Property(p => p.Status).IsRequired().HasColumnType("int");

            // đang xem sét quan hệ khóa ngoại
            builder.HasOne(x => x.Role_Model).WithMany(x => x.User_Models)
               .HasForeignKey(x => x.RoleId);
        }
    }
}
