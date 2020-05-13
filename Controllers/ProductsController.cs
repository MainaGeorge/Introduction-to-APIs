using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToArrayAsync();

            return Ok(products);
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