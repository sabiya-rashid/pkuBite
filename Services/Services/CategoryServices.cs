using AutoWrapper.Wrappers;
using Common.DTOs.Category;
using Microsoft.AspNetCore.Http.HttpResults;
using Models.Base;
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
    public class CategoryServices : ICategoryServices
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

        public async Task<ApiResponse> GetCategory(int id)
        {
            var category = _repository.EntityExists(id);
            if (category == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No category fond with this Id"
                };
                return await Task.FromResult(response);
            }
            var result = _repository.GetEntity(id);
            var res = new ApiResponse
            {
                StatusCode = 200,
                Message = "Category found",
                Result = result
            };
            return await Task.FromResult<ApiResponse>(res);
        }

        public Task<ApiResponse> Remove(int id)
        {
            var category = _repository.EntityExists(id);
            if (category == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No category found with this id"
                };
                return Task.FromResult(res);
            }
            var response = _repository.DeleteEntity(category);
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message = "Category deleted successfully"
            };
            return Task.FromResult((ApiResponse)result);
        }

        public async Task<ApiResponse> UpdateOrAdd(CategoryDto category)
        {
            if (category == null)
                throw new ArgumentNullException("category");
            var payload = new Category
            {
                Name = category.Name
            };
            var cat = _repository.CreateEntity(payload);
            var response = new ApiResponse
            {
                StatusCode = 200,
                Message = "Category added successfully"
            };
            return await Task.FromResult<ApiResponse>(response);
        }

        public async Task<ApiResponse> Update(CategoryDto category)
        {
            var cat = _repository.EntityExists(category.Id);
            if (cat == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No category found with this id"
                };
                return await Task.FromResult(res);
            }
            cat.Name = category.Name;
            var result = _repository.UpdateEntity(cat);
            var response = new ApiResponse
            {
                StatusCode = 200,
                Message = "Category updated successfully"
            };
            return await Task.FromResult<ApiResponse>(response);
        }
    }
}
