using AutoWrapper.Wrappers;
using pkuBite.Models.Models;

namespace pkuBite.Services.IServices
{
    public interface ICategory
    {
        ApiResponse GetAllCategories(string filter, string sort, int pageNo, int pageSize);

        ApiResponse GetById(int id);

        ApiResponse Create(Category item);

        ApiResponse Update(Category item);

        ApiResponse Delete(Category item);
    }
}
