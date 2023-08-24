using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
using pkuBite.Dto;
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSubCategory([FromBody] SubCategoryDto subCategoryDto)
        {
            if (subCategoryDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var subCategory = new SubCategory
            {
                Name = subCategoryDto.Name,
                CategoryId = subCategoryDto.CategoryId                   
            };
            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSubCategory(int id, [FromBody] SubCategoryDto subCategoryDto)
        {
            var subCategory = _context.SubCategories.FirstOrDefault(c => c.Id == id);
            if (subCategory == null)
                return NotFound(ModelState);
            subCategory.Name = subCategoryDto.Name;
            _context.SaveChanges();
            return Ok(subCategory);
        }
    }
}
