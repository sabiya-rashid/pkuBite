using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.Dto;
using pkuBite.Helpers;
using pkuBite.Interfaces;
using pkuBite.Models;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommonRepository<Food> _repository;

        public FoodController(DataContext context, IWebHostEnvironment webHostEnvironment, ICommonRepository<Food> repository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _repository = repository;
        }
        [HttpGet]
        public ActionResult GetAllFoods()
        {
            var foods = _repository.GetEntities();
            return Ok(foods);
        }
        [HttpGet("{id}")]
        public ActionResult GetFood(int id)
        {
            var food = _repository.GetEntity(id);
            return Ok(food);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateFoodItem([FromForm] FoodDto foodDto)
        {
            if (foodDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var flag = _context.SubCategories.Find(foodDto.SubcategoryId);
            if (flag == null)
                return BadRequest("No subcategory with this id");

            if (foodDto.ImageFile != null)
            {
                string imagePath = SaveImage(foodDto.ImageFile);
                var foodItem = new Food
                {
                    Name = foodDto.Name,
                    Description = foodDto.Description,
                    Price = foodDto.Price,
                    SubCategoryId = foodDto.SubcategoryId,
                    ImageUrl = imagePath
                };
                _repository.CreateEntity(foodItem);
            }
            return Ok(ModelState);
        }

        private string SaveImage(IFormFile imageFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/images/" + fileName;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSubCategory(int id, [FromBody] FoodDto foodDto)
        {
            var foodItem = _repository.EntityExists(id);
            var flag = _context.Foods.Find(foodDto.SubcategoryId);
            if (flag == null)
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
            var foods = _context.Foods.Include(s => s.SubCategory).Where(f => f.SubCategoryId == id).ToList();
            if (foods == null)
                return NotFound("No food items with this id");
            return Ok(foods);
        }
    }
}
