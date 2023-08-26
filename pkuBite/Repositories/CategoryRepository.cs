using System;
using pkuBite.Data;
using pkuBite.Interfaces;
using pkuBite.Models;

namespace pkuBite.Repositories
{
    public class CategoryRepository : ICategory, IFeatures<Category>
    {
        private readonly DataContext _db;
        public CategoryRepository(DataContext db)
        {
            _db = db;
        }

        public bool CreateCategory(Category category)
        {
            _db.Add(category);
            return Save();
        }


        public IQueryable<Category> GetAllCategories()
        {
            return _db.Categories;
        }

 
        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public IQueryable<Category> Filter(IQueryable<Category> query, string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(category => category.Name.Contains(filter));
            }
            return query;
        }

        public IQueryable<Category> Pagination(IQueryable<Category> query, int pageNo, int PageSize)
        {
            var skip = (pageNo - 1) * PageSize;
            return query.Skip(skip).Take(PageSize);
        }
        public IQueryable<Category> Sort(IQueryable<Category> query, string sort)
        {
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower())
                {
                    case "name":
                        return query.OrderBy(n=>n.Name);
                    case "Id":
                        return query.OrderBy(n => n.Id);
                };
            }
            return query;
        }
    }
}

