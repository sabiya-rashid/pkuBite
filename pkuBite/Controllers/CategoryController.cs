using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
using pkuBite.Dto;
using pkuBite.Models;

namespace pkuBite.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return Ok(categories);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = new Category
            {
                Name = categoryDto.Name
            };
            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return BadRequest(ModelState);
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
            return Ok(category);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound(ModelState);
            category.Name = categoryDto.Name;
            _context.SaveChanges();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound(ModelState);           
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok("Category deleted");
        }     

    }
}
