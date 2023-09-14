using AutoWrapper.Wrappers;
using Common.DTOs.Category;
using DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Interfaces;
using pkuBite.Models;
using Services.IServices;
using Services.Services;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            return await _categoryServices.GetCategories();
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await _categoryServices.Remove(id);
        }
        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] CategoryDto category)
        {
            return await _categoryServices.Update(category);
        }
      
        [HttpPost("AddCategory")]
        public async Task<ApiResponse> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            return await _categoryServices.UpdateOrAdd(categoryDto);
        }
        [HttpGet("GetCategory")]
        public async Task<ApiResponse> GetCategory(int id)
        { 
          return await _categoryServices.GetCategory(id);
        }
        
    }
}
