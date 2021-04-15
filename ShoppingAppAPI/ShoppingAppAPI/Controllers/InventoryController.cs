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
        // GET: Inventory
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetInventory()
        {
            //return await _context.Inventory.ToListAsync();
            return Ok(DataContext.Inventory);
        }

        // GET: Inventory/5
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

        // PUT: Inventory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(Guid id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        //await _context.SaveChangesAsync();
        //        DataContext.Inventory.FirstOrDefault(x => x.Id.Equals(id))?.Units = product.Units;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: Inventory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();

            DataContext.Inventory.Add(product);

            return Ok(product);
        }

        //// DELETE: api/Inventory/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        //{
        //    var product = await _context.Inventory.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Inventory.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return product;
        //}

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Inventory.Any(e => e.Id == id);
        //}
    }
}
