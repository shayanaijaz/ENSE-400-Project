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
    public class OrderForsController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public OrderForsController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/OrderFors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderFor>>> GetOrderFors()
        {
            return await _context.OrderFors.ToListAsync();
        }

        // GET: api/OrderFors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderFor>> GetOrderFor(int id)
        {
            var orderFor = await _context.OrderFors.FindAsync(id);

            if (orderFor == null)
            {
                return NotFound();
            }

            return orderFor;
        }

        // PUT: api/OrderFors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderFor(int id, OrderFor orderFor)
        {
            if (id != orderFor.OrderForID)
            {
                return BadRequest();
            }

            _context.Entry(orderFor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderForExists(id))
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

        // POST: api/OrderFors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderFor>> PostOrderFor(OrderFor orderFor)
        {
            _context.OrderFors.Add(orderFor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderFor", new { id = orderFor.OrderForID }, orderFor);
        }

        // DELETE: api/OrderFors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderFor>> DeleteOrderFor(int id)
        {
            var orderFor = await _context.OrderFors.FindAsync(id);
            if (orderFor == null)
            {
                return NotFound();
            }

            _context.OrderFors.Remove(orderFor);
            await _context.SaveChangesAsync();

            return orderFor;
        }

        private bool OrderForExists(int id)
        {
            return _context.OrderFors.Any(e => e.OrderForID == id);
        }
    }
}
