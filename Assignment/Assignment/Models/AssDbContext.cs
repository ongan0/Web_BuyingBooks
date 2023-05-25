using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Assignment.Models
{
    public class AssDbContext : DbContext
    {
        public AssDbContext() { }
        public AssDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product_Model> Product_Models { get; set; }
        public DbSet<Bill_Model> Bill_Models { get; set; }
        public DbSet<BillDetail_Model> BillDetail_Models { get; set; }
        public DbSet<Cart_Model> Cart_Models { get; set; }
        public DbSet<Category_Model> Category_Models { get; set; }
        public DbSet<CartDetail_Model> CartDetail_Models { get; set; }
        public DbSet<User_Model> User_Models { get; set; }
        public DbSet<Role_Model> Role_Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=_Cshap4_Ass;Persist Security Info=True;User ID=Healer;Password=1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
