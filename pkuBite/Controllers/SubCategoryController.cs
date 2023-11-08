using AutoWrapper.Wrappers;
using Common.DTOs.SubCategory;
using DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Models;
using Services.IServices;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private readonly ISubcategoryServices _subcategoryServices;

        public SubCategoryController(ISubcategoryServices subcategoryServices)
        {
            _subcategoryServices = subcategoryServices;
        }
        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll() 
        {
            return await _subcategoryServices.GetAllSubcategories();
        }
        [HttpGet]
        public async Task<ApiResponse> GetSubcategory(int id)
        {
            return await _subcategoryServices.GetSubcategory(id);
        }
        [HttpPost]
        public async Task<ApiResponse> CreateSubcategory(SubCategoryDto subcategory)
        {
            return await _subcategoryServices.Create(subcategory);
        }
        [HttpPut]
        public async Task<ApiResponse> UpdateSubcategory(SubCategoryDto subcategory)
        {
            return await _subcategoryServices.Update(subcategory);
        }
        [HttpDelete]
        public async Task<ApiResponse> DeleteSubcategory(int id)
        {
            return await _subcategoryServices.Remove(id);
        }
        [HttpGet("categoryId")]
        public async Task<ApiResponse> GetSubCategoryByCAtegoryId(int id)
        {
          return await _subcategoryServices.GetSubcategoryByCat(id);
        }
    }
}
