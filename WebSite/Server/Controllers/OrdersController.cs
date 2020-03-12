using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Contoso.Online.Orders.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Order.Include(x => x.Items).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Order.Include(x => x.Items)
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("{orderId}/orderReadyToShip")]
        public async Task<ActionResult> ReadyForShipping(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.ReadyForShippingTimeStamp = DateTime.Now;
            _context.Order.Update(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{orderId}/shipped")]
        public async Task<ActionResult> Shipped(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.ShippedTimeStamp = DateTime.Now;
            _context.Order.Update(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{orderId}/updateInventory")]
        public async Task<ActionResult> UpdateInventory(int orderId)
        {
            var order = _context.Order.Include(x => x.Items)
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.Items.ForEach(item => 
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);
                product.InventoryCount = product.InventoryCount - item.Quantity;
                _context.Products.Update(product);
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
    }
}
