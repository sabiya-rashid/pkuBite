using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface IItems
    {
        IQueryable<Food> GetAllItems();

        IEnumerable<Food> GetItemsBySubCategoryId(int id);

        bool CreateItem(Food food);

        bool UpdateItem(Food food);

        bool DeleteItem(Food food);

        SubCategory FindSubCategory(int Id);
        
        Food Find(int id);

        bool Save();

    }
}
