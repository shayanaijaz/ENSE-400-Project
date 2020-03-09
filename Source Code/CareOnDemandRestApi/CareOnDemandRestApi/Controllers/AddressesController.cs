using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareOnDemandRestApi.Models;

namespace CareOnDemandRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public AddressesController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressID }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Customer_AddressesController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public Customer_AddressesController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Customer_Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer_Address>>> GetCustomer_Addresses()
        {
            return await _context.Customer_Addresses.ToListAsync();
        }

        // GET: api/Customer_Address/{CustomerID}
        [HttpGet("{customerID}")]
        public async Task<ActionResult<Customer_Address>> GetCustomer_Address(int customerID)
        {
            var customer_Address = await _context.Customer_Addresses.Where(ca => ca.CustomerID == customerID).ToListAsync();
            if (customer_Address == null)
            {
                return NotFound();
            }

            return Ok(customer_Address);
        }

        // PUT: api/Customer_Address/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer_Address(int id, Customer_Address customer_Address)
        {
            if (id != customer_Address.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(customer_Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_AddressExists(id))
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

        // POST: api/Customer_Address
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Customer_Address>> PostCustomer_Address(Customer_Address customer_Address)
        {
            _context.Customer_Addresses.Add(customer_Address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer_Address", new { id = customer_Address.AddressID }, customer_Address);
        }

        // DELETE: api/Customer_Address/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer_Address>> DeleteCustomer_Address(int id)
        {
            var customer_Address = await _context.Customer_Addresses.FindAsync(id);
            if (customer_Address == null)
            {
                return NotFound();
            }

            _context.Customer_Addresses.Remove(customer_Address);
            await _context.SaveChangesAsync();

            return customer_Address;
        }

        private bool Customer_AddressExists(int id)
        {
            return _context.Customer_Addresses.Any(e => e.AddressID == id);
        }
    }
}
