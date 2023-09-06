//using System;
//using Microsoft.EntityFrameworkCore;
//using pkuBite.Data.Data;
//using pkuBite.Services.IServices;
//using pkuBite.Models.Models;

//namespace pkuBite.Repository.Repositories;

//public class SubCategoryRepository : ISubCategory
//{
//    private readonly DataContext _db;


//    public SubCategoryRepository(DataContext db)
//    {
//        _db = db;

//    }

//    public bool Create(SubCategory item)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Delete(SubCategory item)
//    {
//        throw new NotImplementedException();
//    }

//    public IQueryable<SubCategory> GetAll()
//    {
//        throw new NotImplementedException();
//    }

//    public SubCategory GetById(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public IQueryable<SubCategory> GetSubCategoriesByCategoryId(int id)
//    {
//        return _db.SubCategories.Include(c => c.Category).Where(i => i.Category_id == id);
//    }

//    public bool Update(SubCategory item)
//    {
//        throw new NotImplementedException();
//    }
//}