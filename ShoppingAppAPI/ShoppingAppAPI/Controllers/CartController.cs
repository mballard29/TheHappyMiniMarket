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

        [HttpGet("receipt")]
        public ActionResult<string> GetReceipt()
        {
            string receipt_text = $"Happy MiniMarket Receipt\n\n";
            decimal subtotal = 0m;
            foreach(Product x in DataContext.Cart)
            {
                subtotal += x.Price;
            }
            foreach (Product x in DataContext.Cart)
            {
                receipt_text += $"{x.Name}, {x.Units} units * ${x.UnitPrice} = ${x.Price}\n";
            }
            decimal tax = subtotal * 0.07m;
            receipt_text += $"\nSubtotal:                 ${string.Format("{0:0.00}", subtotal)}\n";
            receipt_text += $"Tax:            7.0%      ${string.Format("{0:0.00}", Decimal.Round(tax, 2))}\n";
            receipt_text += $"Total:                      ${string.Format("{0:0.00}", subtotal + tax)}\n\n";
            receipt_text += "Shop with us again!\n";
            DataContext.Receipt = receipt_text;

            return Ok(DataContext.Receipt);
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
