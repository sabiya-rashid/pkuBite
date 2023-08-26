using System;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.Interfaces;
using pkuBite.Models;

namespace pkuBite.Repositories
{
	public class SubCategoryRepository : ISubCategory, IFeatures<SubCategory>
	{
        private readonly DataContext _db;
       

        public SubCategoryRepository(DataContext db)
		{
            _db = db;
           
        }

        public bool CreateSubCategory(SubCategory subCategory)
        {
            _db.SubCategories.Add(subCategory);
            return Save();
        }

        public bool DeleteSubCategory(SubCategory subCategory)
        {
            var saved = _db.Remove(subCategory);
            return Save();
        }

       

        public SubCategory Find(int id)
        {
            var flag = _db.SubCategories.Find(id);
            return flag;
        }

        public Category FindCategory(int Id)
        {
            var flag = _db.Categories.Find(Id);
            return flag;
        }

        public IQueryable<SubCategory> GetSubCategories()
        {
            return _db.SubCategories;
        }

        public IQueryable<SubCategory> GetSubCategoriesByCategoryId(int id)
        {
            return _db.SubCategories.Include(c => c.Category).Where(i => i.Category_id == id);
        }

        

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

       

        public bool UpdateSubCategory(SubCategory subCategory)
        {
            _db.SubCategories.Update(subCategory);
            return Save();
        }

        public IQueryable<SubCategory> Filter(IQueryable<SubCategory> query, string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(SubCategory => SubCategory.Name.Contains(filter));
            }
            return query;
        }

        public IQueryable<SubCategory> Sort(IQueryable<SubCategory> query, string sort)
        {
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower())
                {
                    case "name":
                        return query.OrderBy(n => n.Name);
                    case "Id":
                        return query.OrderBy(n => n.Id);
                };
            }
            return query;
        }

        public IQueryable<SubCategory> Pagination(IQueryable<SubCategory> query, int pageNo, int PageSize)
        {
            var skip = (pageNo - 1) * PageSize;
            return query.Skip(skip).Take(PageSize);
        }
    }
}

