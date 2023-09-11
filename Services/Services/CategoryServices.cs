using AutoWrapper.Wrappers;
using Common.DTOs.Category;
using Microsoft.AspNetCore.Http.HttpResults;
using Models.Base;
using Repository.Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryServices<Category> : ICategoryServices<Category>
    {
        private readonly IRepository<Category> _repository;
        public CategoryServices(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public Task<ApiResponse> GetCategories()
        {
            var categories = _repository.GetEntities();
            if (categories == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 206,
                    Message = "No Categories found."
                };
                return Task.FromResult(response);
            }
            var res = new ApiResponse
            {
                StatusCode = 200,
                Message = "All categories retrieved.",
                Result = categories

            };
            return Task.FromResult(res);
        }

        public Task<ApiResponse> GetCategory(int id)
        {
            var category = _repository.GetEntity(id);
            if (category == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No category fond with this Id"
                };
            }
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message = "Category found",
                Result = category
            };
            return Task.FromResult<ApiResponse>(result);
        }

        public Task<ApiResponse> Remove(Category category)
        {
            var response = _repository.DeleteEntity(category);
            if (!response)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No category found with this id"
                };
                return Task.FromResult(res);
            }
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message="Category deleted successfully"
            };
            return Task.FromResult((ApiResponse)result);
        }

        public Task<ApiResponse> UpdateOrAdd(CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
