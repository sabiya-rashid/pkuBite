using AutoWrapper.Wrappers;
using pkuBite.Common.DTO;
using pkuBite.Models.Models;

namespace pkuBite.Services.IServices
{
    public interface ISubCategory
    {

        ApiResponse GetAllSubCategories(string filter, string sort, int pageNo, int pageSize);

        ApiResponse GetById(int id);

        ApiResponse Create(subCategoryDTO item);

        ApiResponse Update(subCategoryDTO item);

        ApiResponse Delete(SubCategory item);

        IQueryable<SubCategory> GetSubCategoriesByCategoryId(int id);
    }
}
