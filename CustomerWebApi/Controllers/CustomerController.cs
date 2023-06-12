using CustomerWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAllCustomers()
        //{
        //    var AllCust = await _context.Customers.ToListAsync();
        //    return Ok(AllCust);          
        //}

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            //var AllCust = await _context.Customers.ToListAsync();
            return await _context.Customers.ToListAsync();
        }


        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(u => u.CustomerId == customerId);
            return Ok(customer);
        }


        [HttpPost]
        public async Task<ActionResult> createcustomer([FromBody] Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }


        [HttpPut]
        public async Task<ActionResult> updatecustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }



        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> Delete(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(u => u.CustomerId == customerId);

            if(customerId== null)
            { 
                return NotFound();
            }
             _context.Customers.Remove(customer);   
             await _context.SaveChangesAsync();  

            return NoContent();
        }
    }
}
