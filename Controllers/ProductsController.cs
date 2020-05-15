using System.Linq;
using System.Threading.Tasks;
using HPlusSport.API.HelperClasses;
using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PriceFilter modifier)
        {
            IQueryable<Product> products = _context.Products;

            if (!string.IsNullOrWhiteSpace(modifier.Name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(modifier.Name.ToLower()));
            }

            if (modifier.MaxPrice != null && modifier.MinPrice != null)
            {
                products = products.Where(p => p.Price >= modifier.MinPrice && p.Price <= modifier.MaxPrice);
            }

            else if (modifier.MinPrice != null)
            {
                products = products.Where(p => p.Price >= modifier.MinPrice);
            }
            else if (modifier.MaxPrice != null)
            {
                products = products.Where(p => p.Price <= modifier.MaxPrice);
            }

            products = products
                .Skip(modifier.Size * (modifier.PageNumber - 1))
                .Take(modifier.Size);

            return Ok(await products.ToArrayAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            _context.Add(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
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
                return NotFound("Product does not exist in the database");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product does not exist");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }


}