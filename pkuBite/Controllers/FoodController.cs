using Microsoft.AspNetCore.Mvc;
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
            var foodItem = new Food
            {
                Name = foodDto.Name,
                Description = foodDto.Description,
                Price = foodDto.Price,
                SubcategoryId = foodDto.SubcategoryId
            };
            _context.Foods.Add(foodItem);
            _context.SaveChanges();

            return Ok();
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
    }
}
