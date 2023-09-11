using AutoWrapper.Wrappers;
using Common.DTOs.Category;
using pkuBite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICategoryServices<T>
    {
        Task<ApiResponse> GetCategories();
        Task<ApiResponse> GetCategory(int id);
        Task<ApiResponse> UpdateOrAdd(CategoryDto category);
        Task<ApiResponse> Remove(int id);
    }
}
