using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSport.API.HelperClasses;
using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
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
        public async Task<IActionResult> GetProducts([FromQuery] PriceFilter modifier)
        {
            IQueryable<Product> products = _context.Products;

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
    }
}