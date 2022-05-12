using API11.DTO;
using API11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicatioDbContext _context;

        public OrdersController(ApplicatioDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAsync()
        {
            var Orders = await _context.Orders.ToListAsync();
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound($"No Order was found with ID {id}");
            }

            return Ok(order);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromForm] CreateOrderDto Neworder )
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound($"No Order was found with ID {id}");
            }

            order.Date = Neworder.Date;
            order.TotelPrice=Neworder.TotelPrice;
            order.IsDone = Neworder.IsDone;
            
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        
        [HttpPost]
        public async Task<ActionResult<Category>> CreateOrderAsync([FromForm] CreateOrderDto Neworder)
        {
            var order = new Order {
            Date = Neworder.Date,
           TotelPrice = Neworder.TotelPrice,
            IsDone = Neworder.IsDone,
            
        };
            await _context.AddAsync(order);
            _context.SaveChanges();

            return Ok(order);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

       
    }

}

