using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface ISubCategory
    {
        IQueryable<SubCategory> GetSubCategories();

        IQueryable<SubCategory> GetSubCategoriesByCategoryId(int id);

        bool CreateSubCategory(SubCategory subCategory);

        bool UpdateSubCategory(SubCategory subCategory);

        bool DeleteSubCategory(SubCategory subCategory);

        Category FindCategory(int Id);

        SubCategory Find(int id);

        bool Save();

    }
}
