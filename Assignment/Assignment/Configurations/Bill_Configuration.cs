using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class Bill_Configuration : IEntityTypeConfiguration<Bill_Model>
    {
        public void Configure(EntityTypeBuilder<Bill_Model> builder)
        {
            builder.ToTable("Bill"); //Đặt tên cho bảng
            builder.HasKey(c => c.Id); // set khóa chính
            // set thuột tính cho các cột
            builder.Property(c => c.UserId).
                HasColumnName("UserID").IsRequired();
            builder.Property(c => c.Status).
                HasColumnType("int").IsRequired();
            builder.Property(c => c.CreateDate).
                HasColumnType("Datetime");
            builder.Property(c => c.FullName).
                HasColumnType("Nvarchar(100)");
            builder.Property(c => c.Address).
                HasColumnType("Nvarchar(1000)");
            builder.Property(c => c.PhoneNumber).
                HasColumnType("Varchar(20)");
            builder.Property(c => c.Payment).
                HasColumnType("decimal(18,0)");


            // Khóa ngoại tính sau

            builder.HasOne(p => p.User_Model).WithMany(p => p.Bill_Models).
                HasForeignKey(p => p.UserId).HasConstraintName("FK_Bill_User"); // xem cái UserID

        }
    }
}
