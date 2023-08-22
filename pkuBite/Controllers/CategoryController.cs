using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
using pkuBite.Models;

namespace pkuBite.Controllers
{
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
        
    }
}
