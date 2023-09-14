using AutoWrapper.Wrappers;
using Common.DTOs.FoodItem;
using Common.DTOs.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ISubcategoryServices
    {
        Task<ApiResponse> GetAllSubcategories();
        Task<ApiResponse> GetSubcategory(int id);
        Task<ApiResponse> GetSubcategoryByCat(int categoryId);
        Task<ApiResponse> Create(SubCategoryDto subCategoryDto);
        Task<ApiResponse> Update(SubCategoryDto subCategoryDto);
        Task<ApiResponse> Remove(int id);
    }
}
