using Microsoft.EntityFrameworkCore;

namespace API11.Models
{
    public class ApplicatioDbContext:DbContext
    {
        public  ApplicatioDbContext (DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetailes> OrdersDetailes { get; set; }
      
    }
}
