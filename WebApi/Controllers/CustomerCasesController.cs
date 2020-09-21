using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCasesController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerCasesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CustomerCases
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerCase>>> GetCustomerCase()
        {
            return await _context.CustomerCase.Include(c => c.Customer).Include(c => c.User).ToListAsync();
        }


        // GET: api/CustomerCases/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerCase>> GetCustomerCase(int id)
        {
            var customerCase = await _context.CustomerCase.FindAsync(id);

            if (customerCase == null)
            {
                return NotFound();
            }

            return customerCase;
        }

        // PUT: api/CustomerCases/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerCase(int id, CustomerCase customerCase)
        {
            if (id != customerCase.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerCase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerCaseExists(id))
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







        // POST: api/CustomerCases
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CustomerCase>> PostCustomerCase(CustomerCase customerCase)
        {
            customerCase.Created = DateTime.Now;

            _context.CustomerCase.Add(customerCase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerCase", new { id = customerCase.Id }, customerCase);
        }

        // DELETE: api/CustomerCases/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerCase>> DeleteCustomerCase(int id)
        {
            var customerCase = await _context.CustomerCase.FindAsync(id);
            if (customerCase == null)
            {
                return NotFound();
            }

            _context.CustomerCase.Remove(customerCase);
            await _context.SaveChangesAsync();

            return customerCase;
        }

        [AllowAnonymous]
        [HttpDelete("all")]
        public async Task<ActionResult> DeleteAllCases()
        {
            foreach (var c in _context.CustomerCase)
            {
                _context.CustomerCase.Remove(c);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }


        private bool CustomerCaseExists(int id)
        {
            return _context.CustomerCase.Any(e => e.Id == id);
        }
    }
}
