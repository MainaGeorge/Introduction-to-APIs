using System.Linq;
using System.Threading.Tasks;
using HPlusSport.API.HelperClasses;
using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
    //this is a new api version that gets only products that are in stock
    [ApiVersion("2.0")]
    [Route("products")]
    [ApiController]
    public class ProductVersion2Controller : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductVersion2Controller(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PriceFilter modifier)
        {
            IQueryable<Product> products = _context.Products.Where(p => p.IsAvailable);

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
                return NotFound("item does not exist");

            if (!product.IsAvailable)
                return NotFound("item out of stock");

            return Ok(product);
        }
    }
}