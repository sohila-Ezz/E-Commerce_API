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
    public class OrderDetailesController : ControllerBase
    {
        private readonly ApplicatioDbContext _context;

        public OrderDetailesController(ApplicatioDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailes>>> GetOrdersDetailesAsync()
        {
            var OrdersDetailes = await _context.OrdersDetailes.ToListAsync();
            return Ok(OrdersDetailes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailes>> GetOrderDetailesAsync(int id)
        {
            var orderDetailes = await _context.OrdersDetailes.FindAsync(id);

            if (orderDetailes == null)
            {
                return NotFound($"No OrderDetailes was found with ID {id}");
            }

            return Ok(orderDetailes);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetailesAsync(int id, [FromForm] CreateOrderDetailesDto NewOrderDetailes)
        {
            var orderDetailes = await _context.OrdersDetailes.FindAsync(id);
            if (orderDetailes == null)
            {
                return NotFound($"No OrderDetailes was found with ID {id}");
            }


            orderDetailes.ProductId = NewOrderDetailes.ProductId;
            orderDetailes.ProductQuantity = NewOrderDetailes.ProductQuantity;
            orderDetailes.ProductPrice = NewOrderDetailes.ProductPrice;
            await _context.SaveChangesAsync();
            return Ok(orderDetailes);
        }


        [HttpPost]
        public async Task<ActionResult<Category>> CreateOrderDetailesAsync([FromForm] CreateOrderDetailesDto NewOrderDetailes)
        {
            var orderDetailes = new OrderDetailes
            {
               ProductId = NewOrderDetailes.ProductId,
       ProductQuantity = NewOrderDetailes.ProductQuantity,
          ProductPrice = NewOrderDetailes.ProductPrice
        };
            await _context.AddAsync(orderDetailes);
            _context.SaveChanges();

            return Ok(orderDetailes);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailesAsync(int id)
        {
            var orderDetailes = await _context.OrdersDetailes.FindAsync(id);
            if (orderDetailes == null)
            {
                return NotFound();
            }

            _context.Remove(orderDetailes);
            await _context.SaveChangesAsync();

            return Ok(orderDetailes);
        }
    }
}
