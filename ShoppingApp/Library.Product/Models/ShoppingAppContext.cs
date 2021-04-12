using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class ShoppingAppContext : DbContext
    {
        public ShoppingAppContext(DbContextOptions<ShoppingAppContext> options)
            : base(options)
        {
        }

        public DbSet<Product> TodoItems { get; set; }
    }
}