using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.Dto;
using pkuBite.Models;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly DataContext _context;

        public FoodController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAllFoods() {
            List<Food> foods = _context.Foods.ToList();
            return Ok(foods);
        }
        [HttpGet("{id}")]
        public ActionResult GetFood(int id)
        {
            var food = _context.Foods.FirstOrDefault(f => f.Id == id);
            return Ok(food);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateFoodItem([FromBody] FoodDto foodDto)
        {
            if (foodDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var flag = _context.SubCategories.Find(foodDto.SubcategoryId);
            if (flag == null)
                return BadRequest("No subcategory with this id");
            var foodItem = new Food
            {
                Name = foodDto.Name,
                Description = foodDto.Description,
                Price = foodDto.Price,
                SubCategoryId = foodDto.SubcategoryId
            };
            _context.Foods.Add(foodItem);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSubCategory(int id, [FromBody] FoodDto foodDto)
        {
            var foodItem = _context.Foods.FirstOrDefault(c => c.Id == id);
            var flag = _context.Foods.Find(foodDto.SubcategoryId);
            if(flag == null)
                return BadRequest("Subcategory with this id doesn't exist");
            if (foodItem == null)
                return NotFound(ModelState);
            foodItem.Name = foodDto.Name;
            foodItem.SubCategoryId = foodDto.SubcategoryId;
            _context.SaveChanges();
            return Ok(foodItem);
        }
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFootItem(int id, [FromBody] FoodDto foodDto)
        {
            var foodItem = _context.Foods.FirstOrDefault(c => c.Id == id);
            if (foodItem == null)
                return NotFound(ModelState);
            foodItem.Name = foodDto.Name;
            _context.SaveChanges();
            return Ok(foodItem);
        }
        [HttpGet("subCategoryId")]
        public IActionResult GetFoodItemsBySubCategoryId(int id)
        {
            var foods =  _context.Foods.Include(s => s.SubCategory).Where(f => f.SubCategoryId == id).ToList();
            if(foods == null) 
                return NotFound("No food items with this id");
            return Ok(foods);  
        }
    }
}
