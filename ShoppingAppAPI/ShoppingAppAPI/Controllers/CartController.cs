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

        // GET: Cart
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetCart()
        {
            return Ok(DataContext.Cart);
        }

        // GET: Cart/5
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

        // PUT: Cart/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: Cart
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();

            DataContext.Cart.Add(product);

            return Ok(product);
        }

        // DELETE: Cart/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await _context.Inventory.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        // DELETE: Cart/5
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
