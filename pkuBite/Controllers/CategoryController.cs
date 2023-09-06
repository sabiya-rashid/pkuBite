
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pkuBite.Services.IServices;
using pkuBite.Models.Models;
using AutoWrapper.Wrappers;

namespace pkuBite.Controllers
{
    [ApiController]
    [Route("api/Category")]
    public class CategoryController : ControllerBase
    {
     
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        // --- GET ALL CATEGORIES --- //
       
        [HttpGet("GetAll")]
        public ApiResponse GetCategories(
            [FromQuery] string? filter,
            [FromQuery] string? sort,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize= 5)
        {
            return _category.GetAllCategories(filter, sort, pageNo, pageSize);
        }


        // -----  GET ITEM BY ID ----- //

        [HttpGet("GetBy{Id}")]
        public ApiResponse GetItemById(int Id)
        {
            ApiResponse response = _category.GetById(Id);
            if(response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                return new ApiResponse(response);
            }
        }


        // --- CREATE NEW CATEGORIES --- //
        [Authorize]
        [HttpPost("Create")]
        public ApiResponse CreateCategory([FromBody] Category category)
        {
            return _category.Create(category);
        }

        // ----- UPDATE SUB-CATEGORY ---- //
        [Authorize]
        [HttpPut("Update{Id}")]
        public ApiResponse UpdateSubCategory(int Id, [FromBody] Category category)
        {
           var response = _category.GetById(Id);

            if (response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                return _category.Update(category);
            }
        }

        // ----- DELETE SUB-CATEGORY ---- //
        [Authorize(Roles = "admin")]
        [HttpDelete("Delete{Id}")]
        public ApiResponse Delete(int Id)
        {
            var response = _category.GetById(Id);

            if (response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                Category category = (Category)response.Result;
                _category.Delete(category);
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
        }
    }
}

