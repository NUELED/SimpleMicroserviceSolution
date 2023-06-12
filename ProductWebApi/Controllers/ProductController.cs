using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context; 
        public ProductController( ProductDbContext context)
        {
            _context = context;    
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllCustomers()
        {
            //var AllCust = await _context.Customers.ToListAsync();
            return await _context.Products.ToListAsync();
        }


        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == productId);
            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult> createcustomer([FromBody] Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }


        [HttpPut]
        public async Task<ActionResult> updatecustomer([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }



        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = _context.Products.FirstOrDefault(u => u.ProductId == productId);

            if (productId == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }












    }
}
