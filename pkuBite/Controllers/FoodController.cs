using AutoWrapper.Wrappers;
using Common.DTOs.FoodItem;
using DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Interfaces;
using pkuBite.Models;
using Services.IServices;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IFoodServices _foodservices;
        public FoodController(IFoodServices foodServices)
        {
            _foodservices = foodServices;    
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllFoods()
        {
           return await _foodservices.GetAllFoodItems();
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetFood(int id)
        {
            return await _foodservices.GetFoodItem(id);
        }
        [HttpPost]
        public async Task<ApiResponse> CreateFoodItem([FromForm] FoodItemDto foodDto)
        {
            return await _foodservices.Create(foodDto);
        }        

        //[HttpPut("{id}")]       
        //public IActionResult UpdateSubCategory(int id, [FromBody] FoodItemDto foodDto)
        //{
        //    var foodItem = _repository.EntityExists(id);
        //    var flag = _context.Foods.Find(foodDto.SubcategoryId);
        //    if (flag == null)
        //        return BadRequest("Subcategory with this id doesn't exist");
        //    if (foodItem == null)
        //        return NotFound(ModelState);
        //    foodItem.Name = foodDto.Name;
        //    foodItem.SubCategoryId = foodDto.SubcategoryId;
        //    _context.SaveChanges();
        //    return Ok(foodItem);
        //}

        [HttpPost("{id}")]
        public async Task<ApiResponse> UpdateFootItem([FromBody] FoodItemDto foodDto)
        {
            return await _foodservices.Update(foodDto);
        }

        [HttpGet("subCategoryId")]
        public async Task<ApiResponse> GetFoodItemsBySubCategoryId(int id)
        {
           return await _foodservices.GetFoodItemBySub(id);
        }
    }
}
