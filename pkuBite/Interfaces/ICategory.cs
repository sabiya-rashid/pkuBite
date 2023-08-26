using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface ICategory
    {
        IQueryable<Category> GetAllCategories();

        bool CreateCategory(Category category);

        bool Save();
    }
}
