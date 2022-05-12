using API11.DTO;
using API11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicatioDbContext _context;
        private new List<string> allowExxtention = new List<string> { ".png", ".jpg" };

        public ProductsController(ApplicatioDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductesAsync()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIDAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound($"No Product was found with ID {id}");
            }

            return Ok(product);
        }
        [HttpGet("GetProductByCategorID")]
        public async Task<ActionResult<Product>> GetProductByCategorIDAsync(int CatID)
        {
            var products = await _context.Products.Where(p => p.Category_Id == CatID).ToListAsync();

            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProductAsync([FromForm] CreateProductDto pro)
        {
            if(pro.Image == null) return BadRequest("Image Is Required ");
            if (!allowExxtention.Contains(Path.GetExtension(pro.Image.FileName).ToLower()))
                return BadRequest("Onl .png or .jpg images are allow ");
            var isValidCategory = await _context.categories.AnyAsync(c => c.Id == pro.Category_Id);
            if (!isValidCategory)
                return BadRequest("Not Valide Category Id ");
            using var dataStream = new MemoryStream();
            await pro.Image.CopyToAsync(dataStream);
            var product = new Product
            {

                Name = pro.Name,
                price = pro.price,
                Quantity = pro.Quantity,
                Image = dataStream.ToArray(),
                Description = pro.Description,
                Category_Id = pro.Category_Id


            };
            await _context.AddAsync(product);
            _context.SaveChanges();

            return Ok(product);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromForm] CreateProductDto product)
        {
            var pro = await _context.Products.FindAsync(id);
            if (pro == null)
                return NotFound($"No Product was found with ID {id}");
            var isValidCategory = await _context.categories.AnyAsync(c => c.Id == pro.Category_Id);
            if (!isValidCategory)
                return BadRequest("Not Valide Category Id ");
            if (product.Image != null)
            {
                if (!allowExxtention.Contains(Path.GetExtension(product.Image.FileName).ToLower()))
                    return BadRequest("Onl .png or .jpg images are allow ");
                using var dataStream = new MemoryStream();
                await product.Image.CopyToAsync(dataStream);
                pro.Image = dataStream.ToArray();
            }
            pro.Name = product.Name;
            pro.price = product.price;
            pro.Quantity = product.Quantity;
            pro.Description = product.Description;
            pro.Category_Id = product.Category_Id;
            await _context.SaveChangesAsync();
            return Ok(pro);
        }

    

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

    }
}
