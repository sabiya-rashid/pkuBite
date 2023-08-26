using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.DTO;
using pkuBite.Interfaces;
using pkuBite.Models;
using pkuBite.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pkuBite.Controllers
{
    [Route("api/food")]
    public class FoodController : Controller
    {
        //private readonly DataContext _db;

        //public FoodController(DataContext db)
        //{
        //    _db = db;
        //}

        private readonly IItems _itemRepository;
        private readonly IFeatures<Food> _featuresRepository;

        public FoodController(IItems itemRepository, IFeatures<Food> featuresRepository)
        {
            _itemRepository = itemRepository;
            _featuresRepository = featuresRepository;
        }

        // ----- GET ALL ITEMS ---- //
        [HttpGet]
        public IActionResult GetItems(
            [FromQuery] string? filter,
            [FromQuery] string? sort,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 5)
        {
            IQueryable<Food> items = _itemRepository.GetAllItems();

            items =  _featuresRepository.Filter(items, filter);
            items = _featuresRepository.Sort(items, sort);
            items = _featuresRepository.Pagination(items, pageNo, pageSize);

            var response = new
            {
                StatusCode = 201,
                Message = "Success",
                Data = items
            };

            return new ObjectResult(response)
            {
             StatusCode = StatusCodes.Status200OK
            };
    }

        // -----  GET ITEM BY SUB-CATEGORY ID ----- //

        [HttpGet("{SubcategoryId}")]
        public IEnumerable<Food> GetItemsBySubcategory(int SubcategoryId)
        {
             return _itemRepository.GetItemsBySubCategoryId(SubcategoryId);
            //Foods.Include(c => c.SubCategory).Where(i => i.SubCategoryId == SubcategoryId);
        }

        // --- CREATE A NEW ITEM ---- //
        [Authorize]
        [HttpPost]
        public IActionResult CreateItem([FromBody]FoodDTO foodDTO)
        {
            var flag = _itemRepository.Find(foodDTO.SubCategoryId);
            //var flag = _db.SubCategories.Find(foodDTO.SubCategoryId);
            if (foodDTO == null || flag == null)
            {
                return BadRequest("Body or Subcategory doesnot exist");
            }

            var foodItem = new Food
            {
                Name = foodDTO.Name,
                Description = foodDTO.Description,
                SubCategoryId = foodDTO.SubCategoryId,
            };

            var saved = _itemRepository.CreateItem(foodItem);

            if(saved == false)
            {
                return BadRequest("Something went wrong while creating item");
            }
            var response = new
            {
                StatusCode= 201,
                Message = "Food Item Added",
                Data = foodItem
            };
            //_db.Foods.Add(foodItem);
            //_db.SaveChanges();

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }


         //--- UPDATE ITEM ----- //
        [Authorize]
        [HttpPut("{ItemId}")]
        public IActionResult UpdateItem(int ItemId, [FromBody] FoodDTO foodDTO)
        {
            var item = _itemRepository.Find(ItemId);
            var flag = _itemRepository.FindSubCategory(foodDTO.SubCategoryId);

            if (item == null || flag == null)
            {
                return BadRequest("Item or Subcategory doesn't exist");
            }
            var foodItem = new Food
            {
                Name = foodDTO.Name,
                Description = foodDTO.Description,
                SubCategoryId = foodDTO.SubCategoryId
            };

            var saved = _itemRepository.UpdateItem(foodItem);

            if (saved == false)
            {
                return BadRequest("Something went wrong while creating item");
            }

            var response = new
            {
                StatusCode = 202,
                Message = "Food Item Added",
                Data = foodItem
            };

            //_db.Foods.Update(itemObj);
            //_db.SaveChanges();
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status202Accepted
            };
        }

        // ------ DELETE ITEM ----- //

        [Authorize(Roles = "admin")]
        [HttpDelete("{ItemId}")]
        public IActionResult DeleteItem(int ItemId)
        {
            var item = _itemRepository.Find(ItemId);
            if (item == null )
            {
                return BadRequest("Item doesn't exist");
            }
           
            var saved = _itemRepository.DeleteItem(item);

            if (saved == false)
            {
                return BadRequest("Something went wrong while creating item");
            }

            var response = new
            {
                StatusCode = 204,
                Message = "Food Item Deleted",
                Data = item
            };

            //_db.Foods.Update(itemObj);
            //_db.SaveChanges();
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
    }
}

