using System;
using pkuBite.Data.Data;
using pkuBite.Models.Models;
using pkuBite.Services.IServices;
using AutoWrapper.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;

namespace Services.Services
{
	public class CategoryService : ICategory
	{
        private readonly DataContext _db;
        private readonly IRepository<Category> _repository;
        private readonly IFeatures<Category> _features;

        public CategoryService(
            DataContext db,
            IRepository<Category> repository,
            IFeatures<Category> features)
		{
            _db = db;
            _repository = repository;
            _features = features;
        }

        public ApiResponse Create(Category item)
        {
            if (item is null)
            {
                return new ApiResponse { IsError = true, Message = "Body is null", StatusCode = (int)HttpStatusCode.NotFound };
            }

            bool saved = _repository.Create(item);

            if (saved is false)
            {
                return new ApiResponse { IsError = true, Message = "Something went wrong", StatusCode = (int)HttpStatusCode.InternalServerError };
            }

            return new ApiResponse {Result = item, StatusCode = (int)HttpStatusCode.Accepted, Message = "Added Succesfully" };
        }

        public ApiResponse Delete(Category item)
        {

            if (_repository.Delete(item) is true)
            {
                return new ApiResponse { StatusCode = (int)HttpStatusCode.NoContent, Message = "Deleted Succesfully" };
            }
            else
            {
                return new ApiResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Something went wrong" };
            }
        }

        public ApiResponse GetAllCategories(string filter, string sort, int pageNo, int pageSize)
        {
            IQueryable<Category> query = _repository.GetAll();
            query = _features.Filter(query, filter);
            query = _features.Sort(query, sort);
            query = _features.Pagination(query, pageNo, pageSize);
       
            return new ApiResponse { Result =query, StatusCode = (int)HttpStatusCode.Accepted };
        }

        public ApiResponse GetById(int id)
        {
            Category category = _repository.GetById(id);

            if(category is null)
            {
                return new ApiResponse { IsError = true, Message = "Item Doesn't Exist", StatusCode = (int)HttpStatusCode.NotFound };
            }
            
            return new ApiResponse { Result = category, StatusCode = (int)HttpStatusCode.OK };
        }

        public ApiResponse Update(Category item)
        {
            if (_repository.Update(item) is true)
            {
                return new ApiResponse { StatusCode = (int)HttpStatusCode.Accepted, Message = "Updated Succesfully" };
            }
            else
            {
                return new ApiResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Something went wrong" }; ;
            }
        }
    }
}

