using System;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.Interfaces;
using pkuBite.Models;

namespace pkuBite.Repositories
{
    public class ItemRepository : IItems, IFeatures<Food>
    {
        private readonly DataContext _db;

        public ItemRepository(DataContext db)
        {
            _db = db;
            
        }
        public bool CreateItem(Food food)
        {
            _db.Foods.Add(food);
            return Save();
        }

        public IQueryable<Food> GetAllItems()
        {
            return _db.Foods;
        }

        public IEnumerable<Food> GetItemsBySubCategoryId(int SubCategoryId)
        {
            return _db.Foods.Include(c => c.SubCategory).Where(i => i.SubCategoryId == SubCategoryId);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public SubCategory FindSubCategory(int Id)
        {

            var flag = _db.SubCategories.Find(Id);
            return flag;
        }

        public Food Find(int Id)
        {

            var flag = _db.Foods.Find(Id);
            return flag;
        }

        public bool UpdateItem(Food food)
        {
            _db.Foods.Update(food);
            return Save();
        }

        public bool DeleteItem(Food food)
        {
            var saved = _db.Remove(food);
            return Save();
        }

        public IQueryable<Food> Filter(IQueryable<Food> query, string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(category => category.Name.Contains(filter));
            }
            return query;
        }

        public IQueryable<Food> Pagination(IQueryable<Food> query, int pageNo, int PageSize)
        {
            var skip = (pageNo - 1) * PageSize;
            return query.Skip(skip).Take(PageSize);
        }

        public IQueryable<Food> Sort(IQueryable<Food> query, string sort)
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
    }
}

