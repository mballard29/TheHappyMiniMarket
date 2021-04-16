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
    public class CartController : ControllerBase
    {
        private readonly ShoppingAppContext _context;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetCart()
        {
            return Ok(DataContext.Cart);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(Guid id)
        {
            var product = DataContext.Cart.FirstOrDefault(x=> x.Id.Equals(id));

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

            DataContext.Cart.Add(product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(Guid id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest();
            }

            DataContext.Cart.FirstOrDefault(x => x.Id.Equals(id)).Units = product.Units;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(Guid id)
        {
            var product = DataContext.Cart.FirstOrDefault(x => x.Id.Equals(id));
            if (product == null)
            {
                return NotFound();
            }

            DataContext.Cart.Remove(product);

            return Ok(product);
        }

        [HttpDelete("Clear")]
        public ActionResult<List<Product>> ClearProduct()
        {
            DataContext.Cart.Clear();

            return DataContext.Cart;
        }

        private bool ProductExists(Guid id)
        {
            return DataContext.Cart.Any(e => e.Id == id);
        }
    }
}
