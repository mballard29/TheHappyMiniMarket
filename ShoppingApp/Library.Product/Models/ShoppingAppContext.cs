using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class ShoppingAppContext : DbContext
    {
        public  ShoppingAppContext(DbContextOptions<ShoppingAppContext> options) : base(options)
        {

        }

        public DbSet<Product> Inventory { get; set; }
        public DbSet<Product> ShoppingCart { get; set; }
    }
}
