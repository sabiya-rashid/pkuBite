using AutoWrapper.Wrappers;
using Common.DTOs.Category;
using Common.DTOs.FoodItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFoodServices
    {
        Task<ApiResponse> GetAllFoodItems();
        Task<ApiResponse> GetFoodItem(int id);
        Task<ApiResponse> GetFoodItemBySub(int subCategoryId);
        Task<ApiResponse> Create(FoodItemDto category);
        Task<ApiResponse> Update(FoodItemDto category);
        Task<ApiResponse> Remove(int id);
    }
}
