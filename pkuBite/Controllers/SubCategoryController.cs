using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
using pkuBite.Models;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private readonly DataContext _context;

        public SubCategoryController(DataContext context)
        {
            _context = context;  
        }
        [HttpGet]
        public IActionResult GetAllSubCategories()
        {
            List<SubCategory> subCategories = _context.SubCategories.ToList();
            return Ok(subCategories);
        }
    }
}
