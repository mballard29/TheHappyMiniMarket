using System;
using System.Collections.Generic;
using Library.Models;

namespace ShoppingAppAPI
{
    public class DataContext
    {
        public static List<Product> Inventory = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Avocado", Description = "Hass Avocado", Units = 10, UnitPrice = 0.58m },
            new Product { Id = Guid.NewGuid(), Name = "Bread", Description = "Sunbeam Giant Enriched Bread", Units = 10, UnitPrice = 2.97m },
            new Product { Id = Guid.NewGuid(), Name = "Creamer", Description = "International Delight Caramel Macchiato Creamer, 32 oz.", Units = 10, UnitPrice = 2.77m },
            new Product { Id = Guid.NewGuid(), Name = "Juice", Description = "AriZona Kiwi Mucho Mango Juie Cocktail, 128 fl. oz.", Units = 10, UnitPrice = 2.96m },
            new Product { Id = Guid.NewGuid(), Name = "Pasta", Description = "Barilla Classic Blue Box Spaghetti Pasta, 16 oz.", Units = 10, UnitPrice = 1.28m },
            new Product { Id = Guid.NewGuid(), Name = "Marinara Sauce", Description = "Prego Pasta Sauce, Traditional Italian Tomato Sauce", Units = 10, UnitPrice = 1.88m },
            new Product { Id = Guid.NewGuid(), Name = "Popcorn", Description = "Orville Redenbacher's Original Gourmet Yellow Popcorn Kernels", Units = 10, UnitPrice = 4.98m },
            new Product { Id = Guid.NewGuid(), Name = "Potato Chips", Description = "Cape Code Potato Chips, Original Kettle Cooked Chips, 14 oz.", Units = 10, UnitPrice = 3.98m },
            new Product { Id = Guid.NewGuid(), Name = "Pretzel Crisps", Description = "Snack Factory Pretzel Crisps - 3 oz.", Units = 10, UnitPrice = 1.33m },
            new Product { Id = Guid.NewGuid(), Name = "Water Bottles", Description = "Zephyrhills Brand 100% Natural Spring Water, 16.9 oz. plastic bottles (Pack of 12)", Units = 10, UnitPrice = 2.48m },
            new Product { Id = Guid.NewGuid(), Name = "Apple", Description = "Gala Apple", Units = 10, UnitPrice = 1.18m },
            new Product { Id = Guid.NewGuid(), Name = "Bell Pepper", Description = "Green Bell Pepper", Units = 10, UnitPrice = 0.74m },
            new Product { Id = Guid.NewGuid(), Name = "Cantaloupe", Description = "Cantaloupe", Units = 10, UnitPrice = 2.38m },
            new Product { Id = Guid.NewGuid(), Name = "Clemetine", Description = "Clementine", Units = 10, UnitPrice = 1.44m },
            new Product { Id = Guid.NewGuid(), Name = "Coffee", Description = "Folgers Classic Roast Ground Coffee, Medium Roast", Units = 10, UnitPrice = 0.22m },
            new Product { Id = Guid.NewGuid(), Name = "Onions", Description = "Yellow Onions", Units = 10, UnitPrice = 0.7m },
            new Product { Id = Guid.NewGuid(), Name = "Pineapple", Description = "Pineapple", Units = 10, UnitPrice = 2.27m },
            new Product { Id = Guid.NewGuid(), Name = "Tomatoes", Description = "Roma Tomatoes", Units = 10, UnitPrice = 0.29m },
            new Product { Id = Guid.NewGuid(), Name = "Trail Mix", Description = "GourmetNut Power Up Mega Omega Trail Mix", Units = 10, UnitPrice = 0.36m },
            new Product { Id = Guid.NewGuid(), Name = "Lemons", Description = "Lemons", Units = 10, UnitPrice = 0.54m },
            new Product { Id = Guid.NewGuid(), Name = "Avocado", Description = "Maluma Avocado", Units = 10, UnitPrice = 0.58m },
            new Product { Id = Guid.NewGuid(), Name = "Avocado", Description = "Persea americana Avocado", Units = 10, UnitPrice = 0.58m }
        };

        public static List<Product> Cart = new List<Product>();

        public static string Receipt;
    }
}
