using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface ISubCategoryRepository
    {
        ICollection<SubCategory> GetSubCategories();
        SubCategory GetSubCategory(int id);
        ICollection<Food> GetFoodBySubcategory(int subCategoryId);
        bool SubCategoryExists(int id);
        bool CreateSubCategory(SubCategory category);
        bool Save();
        bool DeleteSubCategory(int id);
        bool UpdateSubCategory(SubCategory category);
    }
}
