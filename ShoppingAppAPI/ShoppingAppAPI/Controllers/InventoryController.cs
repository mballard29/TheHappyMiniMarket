using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace ShoppingAppAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ShoppingAppContext _context;

        // GET: Inventory
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetInventory()
        {
            return Ok(DataContext.Inventory);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(Guid id)
        {
            var product = DataContext.Inventory.FirstOrDefault(x => x.Id.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            DataContext.Inventory.Add(product);

            return Ok(product);
        }

        [HttpPost("Range")]
        public ActionResult<IEnumerable<Product>> PostRangeProduct([FromBody] IEnumerable<Product> list)
        {
            if (list == null)
                return BadRequest();

            foreach(Product x in list)
            {
                DataContext.Inventory.Add(new Product(x));
            }

            return Ok(list);
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(Guid id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest();
            }

            DataContext.Inventory.FirstOrDefault(x => x.Id.Equals(id)).Units = product.Units;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(Guid id)
        {
            var product = DataContext.Inventory.FirstOrDefault(x => x.Id.Equals(id));
            if (product == null)
            {
                return NotFound();
            }

            DataContext.Inventory.Remove(product);

            return Ok(product);
        }

        [HttpDelete("Clear")]
        public ActionResult<List<Product>> ClearProduct()
        {
            DataContext.Inventory.Clear();

            return DataContext.Inventory;
        }

        private bool ProductExists(Guid id)
        {
            return DataContext.Inventory.Any(e => e.Id == id);
        }
    }
}

