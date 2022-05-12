using API11.DTO;
using API11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicatioDbContext _context;

        public CategoriesController(ApplicatioDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
        {
           var Categories= await _context.categories.ToListAsync();
            return Ok(Categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryAsync(int id)
        {
            var category = await _context.categories.FindAsync(id);

            if (category == null)
            {
                return NotFound($"No category was found with ID {id}");
            }

            return Ok( category);
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromForm] CreateCategoryDto category)
        {
            var cate = await _context.categories.FindAsync(id);
            if (cate == null)
            {
                return NotFound($"No Category was found with ID {id}");
            }
            cate.Name = category.Name;
            await _context.SaveChangesAsync();
           return Ok(cate);
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategoryAsync([FromForm] CreateCategoryDto cate)
        {
            var category=new Category { Name= cate.Name };
            await _context.AddAsync(category);
             _context.SaveChanges();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.categories.Any(e => e.Id == id);
        }
    }
}
