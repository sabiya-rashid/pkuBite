using System.Net;
using AutoWrapper.Wrappers;
using Microsoft.EntityFrameworkCore;
using pkuBite.Common.DTO;
using pkuBite.Data.Data;
using pkuBite.Models.Models;
using pkuBite.Services.IServices;

namespace Services.Services
{
	public class ItemService : IItem
	{
        private readonly DataContext _db;
        private readonly IRepository<Food> _repository;
        private readonly IFeatures<Food> _features;

        public ItemService(DataContext db,
            IRepository<Food> repository,
            IFeatures<Food> features)
		{
            _db = db;
            _repository = repository;
            _features = _features;
		}

        public ApiResponse Create(FoodDTO item)
        {
            if (item is null)
            {
                return new ApiResponse { IsError = true, Message = "Body is null", StatusCode = (int)HttpStatusCode.NotFound };
            }
            Food food = new Food
            {
                Name = item.Name,
                Description = item.Description,
                SubCategoryId = item.SubCategoryId

            };
            bool saved = _repository.Create(food);

            if (saved is false)
            {
                return new ApiResponse { IsError = true, Message = "Something went wrong", StatusCode = (int)HttpStatusCode.InternalServerError };
            }

            return new ApiResponse { Result = item, StatusCode = (int)HttpStatusCode.Accepted, Message = "Added Succesfully" };
        }

        public ApiResponse Delete(Food item)
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

        public ApiResponse GetAllItems(string filter, string sort, int pageNo, int pageSize)
        {
            IQueryable<Food> query = _repository.GetAll();
            query = _features.Filter(query, filter);
            query = _features.Sort(query, sort);
            query = _features.Pagination(query, pageNo, pageSize);

            return new ApiResponse { Result = query, StatusCode = (int)HttpStatusCode.Accepted };
        }

        public ApiResponse GetById(int id)
        {
            Food item = _repository.GetById(id);

            if (item is null)
            {
                return new ApiResponse { IsError = true, Message = "Item Doesn't Exist", StatusCode = (int)HttpStatusCode.NotFound };
            }

            return new ApiResponse { Result = item, StatusCode = (int)HttpStatusCode.OK };
        }

        public IEnumerable<Food> GetItemsBySubCategoryId(int id)
        {
            return _db.Foods.Include(c => c.SubCategory).Where(i => i.SubCategoryId == id);
        }

        public ApiResponse Update(FoodDTO item)
        {
            Food food = new Food
            {
                Name = item.Name,
                Description = item.Description,
                SubCategoryId = item.SubCategoryId

            };

            if (_repository.Update(food) is true)
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

