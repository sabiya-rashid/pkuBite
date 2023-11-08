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

        [HttpPut]
        public async Task<ApiResponse> UpdateFootItem([FromForm] FoodItemDto foodDto)
        {
            return await _foodservices.Update(foodDto);
        }

        [HttpGet("GetBySubcategory")]
        public async Task<ApiResponse> GetFoodItemsBySubCategoryId(int id)
        {
           return await _foodservices.GetFoodItemBySub(id);
        }
    }
}
