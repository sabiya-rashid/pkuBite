using System.Net;
using AutoWrapper.Wrappers;
using Microsoft.EntityFrameworkCore;
using pkuBite.Common.DTO;
using pkuBite.Data.Data;
using pkuBite.Models.Models;
using pkuBite.Services.IServices;

namespace Services.Services
{
	public class SubCategoryService : ISubCategory
	{
        private readonly DataContext _db;
        private readonly IRepository<SubCategory> _repository;
        private readonly IFeatures<SubCategory> _features;

        public SubCategoryService(DataContext db,
            IRepository<SubCategory> repository,
            IFeatures<SubCategory> features)
        {
            _db = db;
            _repository = repository;
            _features = features;
        }

        public ApiResponse Create(subCategoryDTO item)
        {
            SubCategory subCategory = new SubCategory
            {
                Name = item.Name,
                Category_id = item.CategoryId
            };
            if (item is null)
            {
                return new ApiResponse { IsError = true, Message = "Body is null", StatusCode = (int)HttpStatusCode.NotFound };
            }

            bool saved = _repository.Create(subCategory);

            if (saved is false)
            {
                return new ApiResponse { IsError = true, Message = "Something went wrong", StatusCode = (int)HttpStatusCode.InternalServerError };
            }

            return new ApiResponse { Result = item, StatusCode = (int)HttpStatusCode.Accepted, Message = "Added Succesfully" };
        }

        public ApiResponse Delete(SubCategory item)
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

        public ApiResponse GetAllSubCategories(string filter, string sort, int pageNo, int pageSize)
        {
            IQueryable<SubCategory> query = _repository.GetAll();
            query = _features.Filter(query, filter);
            query = _features.Sort(query, sort);
            query = _features.Pagination(query, pageNo, pageSize);

            return new ApiResponse { Result = query, StatusCode = (int)HttpStatusCode.Accepted };
        }

        public ApiResponse GetById(int id)
        {
            SubCategory subCategory = _repository.GetById(id);

            if (subCategory is null)
            {
                return new ApiResponse { IsError = true, Message = "Item Doesn't Exist", StatusCode = (int)HttpStatusCode.NotFound };
            }

            return new ApiResponse { Result = subCategory, StatusCode = (int)HttpStatusCode.OK };
        }

        public IQueryable<SubCategory> GetSubCategoriesByCategoryId(int id)
        {
            return _db.SubCategories.Include(c => c.Category).Where(i => i.Category_id == id);
        }

        public ApiResponse Update(subCategoryDTO item)
        {
            SubCategory subCategory = new SubCategory
            {
                Name = item.Name,
                Category_id = item.CategoryId
            };
            if (_repository.Update(subCategory) is true)
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

