using AutoWrapper.Wrappers;
using Common.DTOs.Category;
using Common.DTOs.FoodItem;
using DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class FoodServices : IFoodServices
    {
        private readonly IRepository<FoodItem> _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DataContext _context;
        public FoodServices(IRepository<FoodItem> repository, IWebHostEnvironment webHostEnvironment, DataContext context)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<ApiResponse> Create(FoodItemDto foodItem)
        {
            if (foodItem == null)
                throw new ArgumentNullException("foodItem");
            var flag = _context.SubCategories.Where(s=> s.Id==foodItem.SubcategoryId).FirstOrDefault();
            if (flag == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No subcategory with this id"
                };
                return await Task.FromResult(res);
            }
            string imagePath = SaveImage(foodItem.ImageFile);
            var payload = new FoodItem
            {
                Name = foodItem.Name,
                Description = foodItem.Description,
                Price = foodItem.Price,
                SubCategoryId = foodItem.SubcategoryId,
                ImageUrl = imagePath
            };
            var cat = _repository.CreateEntity(payload);
            var response = new ApiResponse
            {
                StatusCode = 200,
                Message = "Food item added successfully"
            };
            return await Task.FromResult(response);
        }
        private string SaveImage(IFormFile imageFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/images/" + fileName;
        }

        public Task<ApiResponse> GetAllFoodItems()
        {
            var fooditems = _repository.GetEntities();
            if (fooditems == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 206,
                    Message = "No food items found."
                };
                return Task.FromResult(response);
            }
            var res = new ApiResponse
            {
                StatusCode = 200,
                Message = "All food items retrieved.",
                Result = fooditems

            };
            return Task.FromResult(res);
        }

        public Task<ApiResponse> GetFoodItem(int id)
        {
            var foodItem = _repository.GetEntity(id);
            if (foodItem == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No Food found with this Id"
                };
                return Task.FromResult(response);
            }
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message = "Food item found",
                Result = foodItem
            };
            return Task.FromResult<ApiResponse>(result);
        }

        public async Task<ApiResponse> GetFoodItemBySub(int subCategoryId)
        {
            var fooditem = _context.Foods.Include(s => s.SubCategory).Where(f => f.SubCategoryId == subCategoryId).ToList();
            if (fooditem == null)
            {
                var response = new ApiResponse
                {
                    StatusCode = 200,
                    Message = "No foods"
                };
                return await Task.FromResult<ApiResponse>(response);
            }
            var res = new ApiResponse
            {
                StatusCode = 200,
                Message = "Foods retrieved successfully"
            };
            return await Task.FromResult(res);

        }

        public Task<ApiResponse> Remove(int id)
        {

            var foodItem = _repository.EntityExists(id);
            if (foodItem == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No food found with this id"
                };
                return Task.FromResult(res);
            }
            var response = _repository.DeleteEntity(foodItem);
            var result = new ApiResponse
            {
                StatusCode = 200,
                Message = "Food item deleted successfully"
            };
            return Task.FromResult((ApiResponse)result);
        }

        public Task<ApiResponse> Update(FoodItemDto foodItemDto)
        {
            var foodItem = _repository.EntityExists(foodItemDto.Id);
            if (foodItem == null)
            {
                var res = new ApiResponse
                {
                    StatusCode = 404,
                    Message = "No Food item found with this id"
                };
                return Task.FromResult(res);
            }
            foodItem.Name = foodItemDto.Name;
            var result = _repository.UpdateEntity(foodItem);
            var response = new ApiResponse
            {
                StatusCode = 200,
                Message = "Food item updated successfully"
            };
            return Task.FromResult<ApiResponse>(response);
        }

    }
}
