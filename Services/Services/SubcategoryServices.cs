using AutoWrapper.Wrappers;
using Common.DTOs.FoodItem;
using Common.DTOs.SubCategory;
using DbContext;
using Microsoft.EntityFrameworkCore;
using pkuBite.Models;
using Repository.Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SubcategoryServices : ISubcategoryServices
    {
        DataContext _context;
        IRepository<SubCategory> _repository;
        public SubcategoryServices(IRepository<SubCategory> repository, DataContext context)
        {
            _context = context;
            _repository = repository;
        }
        public Task<ApiResponse> Create(SubCategoryDto subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException("subcategory");
            var flag = _context.Categories.Where(s => s.Id == subCategory.CategoryId).FirstOrDefault();
            if (flag == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No category with this id"
                };
                return Task.FromResult(res);
            }
            var payload = new SubCategory
            {
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId
            };
            var cat = _repository.CreateEntity(payload);
            var response = new ApiResponse
            {
                StatusCode = 200,
                Message = "Subcategory added successfully"
            };
            return Task.FromResult(response);
        }

        public Task<ApiResponse> GetAllSubcategories()
        {
            var subCategories = _repository.GetEntities();
            if (subCategories == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 206,
                    Message = "No subcategories found."
                };
                return Task.FromResult(response);
            }
            var res = new ApiResponse
            {
                StatusCode = 200,
                Message = "All subcategories retrieved.",
                Result = subCategories

            };
            return Task.FromResult(res);
        }

        public Task<ApiResponse> GetSubcategory(int id)
        {
            var subcategory = _repository.GetEntity(id);
            if (subcategory == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No Subcategory found with this Id"
                };
                return Task.FromResult(response);
            }
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message = "Subcategory found",
                Result = subcategory
            };
            return Task.FromResult<ApiResponse>(result);
        }

        public Task<ApiResponse> GetSubcategoryByCat(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Remove(int id)
        {
            var subcategory = _repository.EntityExists(id);
            if (subcategory == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No subcategory found with this id"
                };
                return Task.FromResult(res);
            }
            var response = _repository.DeleteEntity(subcategory);
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message = "Subcategory deleted successfully"
            };
            return Task.FromResult((ApiResponse)result);
        }

        public Task<ApiResponse> Update(SubCategoryDto subCategoryDto)
        {
            var subcategory = _repository.EntityExists(subCategoryDto.Id);
            if (subcategory == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No subcategory found with this id"
                };
                return Task.FromResult(res);
            }
            subcategory.Name = subCategoryDto.Name;
            subcategory.CategoryId = subCategoryDto.CategoryId;
            var result = _repository.UpdateEntity(subcategory);
            var response = new ApiResponse
            {
                StatusCode = 200,
                Message = "Subcategory updated successfully"
            };
            return Task.FromResult<ApiResponse>(response);
        }
    }
}
