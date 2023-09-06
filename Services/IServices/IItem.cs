using AutoWrapper.Wrappers;
using pkuBite.Common.DTO;
using pkuBite.Models.Models;

namespace pkuBite.Services.IServices;

public interface IItem
{
    ApiResponse GetAllItems(string filter, string sort, int pageNo, int pageSize);

    ApiResponse GetById(int id);

    ApiResponse Create(FoodDTO item);

    ApiResponse Update(FoodDTO item);

    ApiResponse Delete(Food item);

    IEnumerable<Food> GetItemsBySubCategoryId(int id);
}
