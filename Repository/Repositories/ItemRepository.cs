//using Microsoft.EntityFrameworkCore;
//using pkuBite.Data.Data;
//using pkuBite.Services.IServices;
//using pkuBite.Models.Models;

//namespace pkuBite.Repository.Repositories;

//public class ItemRepository : IItem
//{
//    private readonly DataContext _db;

//    public ItemRepository(DataContext db)
//    {
//        _db = db;
        
//    }

//    public bool Create(Food item)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Delete(Food item)
//    {
//        throw new NotImplementedException();
//    }

//    public IQueryable<Food> GetAll()
//    {
//        throw new NotImplementedException();
//    }

//    public Food GetById(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<Food> GetItemsBySubCategoryId(int SubCategoryId)
//    {
//        return _db.Foods.Include(c => c.SubCategory).Where(i => i.SubCategoryId == SubCategoryId);
//    }

//    public bool Update(Food item)
//    {
//        throw new NotImplementedException();
//    }
//}

