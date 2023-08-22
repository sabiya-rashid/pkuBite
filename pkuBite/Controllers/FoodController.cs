using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
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
        [HttpGet("{foodId}")]
        public ActionResult GetFood(int id)
        {
            var food = _context.Foods.FirstOrDefault(f => f.Id == id);
            return Ok(food);
        }
    }
}
