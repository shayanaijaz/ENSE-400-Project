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
    public class OrdersController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public OrdersController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: api/Orders/OrderStatus/{orderStatus}
        [HttpGet("OrderStatus/{orderStatus}")]
        public async Task<ActionResult<Order>> GetAccount(int orderStatus)
        {
            var order = await _context.Orders.Where(a => a.OrderStatusID == orderStatus).ToListAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public OrderStatusesController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/OrderStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetOrderStatuses()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        // GET: api/OrderStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus(int id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return orderStatus;
        }

        // PUT: api/OrderStatus/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderStatus(int id, OrderStatus orderStatus)
        {
            if (id != orderStatus.OrderStatusID)
            {
                return BadRequest();
            }

            _context.Entry(orderStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // POST: api/OrderStatus
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> PostOrderStatus(OrderStatus orderStatus)
        {
            _context.OrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderStatus", new { id = orderStatus.OrderStatusID }, orderStatus);
        }

        // DELETE: api/OrderStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderStatus>> DeleteOrderStatus(int id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();

            return orderStatus;
        }

        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Order_ServicesController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public Order_ServicesController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Order_Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order_Service>>> GetOrder_Services()
        {
            return await _context.Order_Services.ToListAsync();
        }

        // GET: api/Order_Service/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order_Service>> GetOrder_Service(int id)
        {
            var order_Service = await _context.Order_Services.FindAsync(id);

            if (order_Service == null)
            {
                return NotFound();
            }

            return order_Service;
        }

        // PUT: api/Order_Service/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder_Service(int id, Order_Service order_Service)
        {
            if (id != order_Service.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order_Service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_ServiceExists(id))
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

        // POST: api/Order_Service
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order_Service>> PostOrder_Service(Order_Service order_Service)
        {
            _context.Order_Services.Add(order_Service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_Service", new { id = order_Service.OrderID }, order_Service);
        }

        // DELETE: api/Order_Service/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order_Service>> DeleteOrder_Service(int id)
        {
            var order_Service = await _context.Order_Services.FindAsync(id);
            if (order_Service == null)
            {
                return NotFound();
            }

            _context.Order_Services.Remove(order_Service);
            await _context.SaveChangesAsync();

            return order_Service;
        }

        private bool Order_ServiceExists(int id)
        {
            return _context.Order_Services.Any(e => e.OrderID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public ServiceRequestsController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/ServiceRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            return await _context.ServiceRequests.ToListAsync();
        }

        // GET: api/ServiceRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return serviceRequest;
        }

        // PUT: api/ServiceRequests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequest(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(serviceRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
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

        // POST: api/ServiceRequests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceRequest", new { id = serviceRequest.OrderID }, serviceRequest);
        }

        // DELETE: api/ServiceRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRequest>> DeleteServiceRequest(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            _context.ServiceRequests.Remove(serviceRequest);
            await _context.SaveChangesAsync();

            return serviceRequest;
        }

        private bool ServiceRequestExists(int id)
        {
            return _context.ServiceRequests.Any(e => e.OrderID == id);
        }
    }
}
