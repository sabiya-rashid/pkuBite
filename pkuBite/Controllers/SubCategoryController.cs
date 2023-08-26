using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.DTO;
using pkuBite.Interfaces;
using pkuBite.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pkuBite.Controllers
{
    [ApiController]
    [Route("api/subCategories")]
    public class SubCategoryController : ControllerBase
    {

        private readonly ISubCategory _subCategory;
        private readonly IFeatures<SubCategory> _features;

        public SubCategoryController(ISubCategory subCategory, IFeatures<SubCategory> features)
        {
            _subCategory = subCategory;
            _features = features;
        }

        // ----- GET ALL SUB CATEGORIES ---- //

        [HttpGet]
        public IActionResult GetSubCategories(
            [FromQuery] string? filter,
            [FromQuery] string? sort,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 5)
        {
            IQueryable<SubCategory> subCategories = _subCategory.GetSubCategories();


            IQueryable<SubCategory> query = _features.Filter(subCategories, filter);
            query = _features.Sort(query, sort);
            query = _features.Pagination(query, pageNo, pageSize);
            var response = new
            {
                StatusCode = 201,
                Message = "Food Item Added",
                Data = query
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
            //return _subCategory.GetSubCategories();
        }


        // -----  GET SUB-CATEGORIES BY CATEGORY ID ----- //

        [HttpGet("{CategoryId}")]
        public IEnumerable<SubCategory> GetSubCategoriesByCategory(int CategoryId)
        {
            return _subCategory.GetSubCategoriesByCategoryId(CategoryId);
            //return _db.SubCategories.Include(c => c.Category).Where(i => i.Category_id == CategoryId);
        }


        // ----- CREATE NEW SUB-CATEGORY ---- //
        [Authorize]
        [HttpPost]
        public IActionResult CreateSubCategory([FromBody] subCategoryDTO subCategoryDTO)
        {
            if (subCategoryDTO == null)
            {
                return BadRequest("Body can't be null");
            }

            var flag = _subCategory.FindCategory(subCategoryDTO.CategoryId);

            //var categories = _db.SubCategories.Include(c => c.Category).ToList();
            if (flag == null)
            {
                return BadRequest("Category doesnpt exist");
            }

            var subCategory = new SubCategory
            { 
                Name = subCategoryDTO.Name,
                Category_id = subCategoryDTO.CategoryId,
            };

            var saved = _subCategory.CreateSubCategory(subCategory);

            if(saved == false)
            {
                return BadRequest("Something went wrong while adding the subcategory");
            }
            var response = new
            {
                StatusCode = 201,
                Message = "Food Item Added",
                Data = subCategory
            };
            //_db.SubCategories.Add(subCategory);
            //_db.SaveChanges();

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }


        // ----- UPDATE SUB-CATEGORY ---- //
        [Authorize]
        [HttpPut("{SubcategoryId}")]
        public IActionResult UpdateSubCategory(int SubcategoryId, [FromBody]subCategoryDTO subCategoryDTO)
        {
            var subCategory = _subCategory.Find(SubcategoryId);
            var flag = _subCategory.FindCategory(subCategoryDTO.CategoryId);

            if (subCategory == null || flag == null)
            {
                return BadRequest("Subcategory or Category doesn't exist");
            }
            var subCategoryObj = new SubCategory
            {
                Name = subCategoryDTO.Name,
                Category_id = subCategoryDTO.CategoryId,
            };
            var saved = _subCategory.UpdateSubCategory(subCategoryObj);

            if (saved == false)
            {
                return BadRequest("Something went wrong while updating the subcategory");
            }
            var response = new
            {
                StatusCode = 202,
                Message = "Food Item Added",
                Data = subCategoryObj
            };

            //_db.SubCategories.Update(subCategoryObj);
            //_db.SaveChanges();
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status202Accepted
            };
        }

        // ----- DELETE SUB-CATEGORY ---- //
        [Authorize(Roles = "admin")]
        [HttpDelete("{Subcategoryid}")]
        public IActionResult Delete(int Subcategoryid)
        {
            var subCategory = _subCategory.Find(Subcategoryid); 

            if(subCategory == null)
            {
                return BadRequest("Subcategory Doesn't exist");
            }

            var saved = _subCategory.DeleteSubCategory(subCategory);
            var response = new
            {
                StatusCode = 202,
                Message = "Food Item Added",
                Data = subCategory
            };

            //_db.SubCategories.Remove(subCategory);
            //_db.SaveChanges();

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
    }
}

