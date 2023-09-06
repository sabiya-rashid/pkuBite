
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pkuBite.Common.DTO;
using pkuBite.Services.IServices;
using pkuBite.Models.Models;
using AutoWrapper.Wrappers;

namespace pkuBite.Controllers
{
    [Route("api/food")]
    public class FoodController : Controller
    {
        private readonly IItem _itemService;
        //private readonly IFeatures<Food> _featuresRepository;
        //private readonly IRepository<Food> _repository;

        public FoodController(IItem itemService)
        {
            _itemService = itemService;
            //_featuresRepository = featuresRepository;
            //_repository = repository;
        }

        // ----- GET ALL ITEMS ---- //
        [HttpGet("GetAll")]
        public ApiResponse GetItems(
            [FromQuery] string? filter,
            [FromQuery] string? sort,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 5)
        {
            return _itemService.GetAllItems(filter, sort, pageNo, pageSize);
        }

        // -----  GET ITEM BY ID ----- //

        [HttpGet("GetById{Id}")]
        public ApiResponse GetItemById(int Id)
        {
            return _itemService.GetById(Id);
        }


        // -----  GET ITEM BY SUB-CATEGORY ID ----- //

        [HttpGet("GetBySubCategoryId{SubcategoryId}")]
        public IEnumerable<Food> GetItemsBySubcategory(int SubcategoryId)
        {
             return _itemService.GetItemsBySubCategoryId(SubcategoryId);
        }


        // --- CREATE A NEW ITEM ---- //
        [Authorize]
        [HttpPost("Create")]
        public ApiResponse CreateItem([FromBody] Common.DTO.FoodDTO foodDTO)
        {
            return _itemService.Create(foodDTO);
        }


         //--- UPDATE ITEM ----- //
        [Authorize]
        [HttpPut("Upate{ItemId}")]
        public ApiResponse UpdateItem(int ItemId, [FromBody] Common.DTO.FoodDTO foodDTO)
        {
            var response = _itemService.GetById(ItemId);

            if (response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                return _itemService.Update(foodDTO);
            }
        }

        // ------ DELETE ITEM ----- //

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete{ItemId}")]
        public ApiResponse DeleteItem(int ItemId)
        {
            var response = _itemService.GetById(ItemId);

            if (response.IsError is true)
            {
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
            else
            {
                Food food = (Food)response.Result;
                _itemService.Delete(food);
                return new ApiResponse { Message = response.Message, StatusCode = response.StatusCode };
            }
        }
    }
}

