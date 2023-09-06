using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pkuBite.Common.DTO;
using pkuBite.Services.IServices;
using pkuBite.Models.Models;
using AutoWrapper.Wrappers;
using Services.Services;

namespace pkuBite.Controllers
{
    [ApiController]
    [Route("api/subCategories")]
    public class SubCategoryController : ControllerBase
    {

        private readonly ISubCategory _subCategory;
        private readonly IFeatures<SubCategory> _features;
        private readonly IRepository<SubCategory> _repository;

        public SubCategoryController(ISubCategory subCategory, IFeatures<SubCategory> features, IRepository<SubCategory> repository)
        {
            _subCategory = subCategory;
            _features = features;
            _repository = repository;
        }

        // ----- GET ALL SUB CATEGORIES ---- //

        [HttpGet("GetAll")]
        public ApiResponse GetSubCategories(
            [FromQuery] string? filter,
            [FromQuery] string? sort,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 5)
        {

            return _subCategory.GetAllSubCategories(filter, sort, pageNo, pageSize);
        }


        // -----  GET ITEM BY ID ----- //

        [HttpGet("GetById{Id}")]
        public ApiResponse GetItemById(int Id)
        {
            return _subCategory.GetById(Id);
        }


        // -----  GET SUB-CATEGORIES BY CATEGORY ID ----- //

        [HttpGet("GetByCategoryId{CategoryId}")]
        public IEnumerable<SubCategory> GetSubCategoriesByCategory(int CategoryId)
        {
            return _subCategory.GetSubCategoriesByCategoryId(CategoryId);
        }


        // ----- CREATE NEW SUB-CATEGORY ---- //
        [Authorize]
        [HttpPost("Create")]
        public ApiResponse CreateSubCategory([FromBody] subCategoryDTO subCategoryDTO)
        {
            return _subCategory.Create(subCategoryDTO);
        }


        // ----- UPDATE SUB-CATEGORY ---- //
        [Authorize]
        [HttpPut("Upadte{SubcategoryId}")]
        public ApiResponse UpdateSubCategory(int SubcategoryId, [FromBody]subCategoryDTO subCategoryDTO)
        {
            var response = _subCategory.GetById(SubcategoryId);

            if (response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                return _subCategory.Update(subCategoryDTO);
            }
        }

        // ----- DELETE SUB-CATEGORY ---- //
        [Authorize(Roles = "admin")]
        [HttpDelete("Delete{Subcategoryid}")]
        public ApiResponse Delete(int Subcategoryid)
        {
            var response = _subCategory.GetById(Subcategoryid);

            if (response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                SubCategory subCategory = (SubCategory)response.Result;
                _subCategory.Delete(subCategory);
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
        }
    }
}

