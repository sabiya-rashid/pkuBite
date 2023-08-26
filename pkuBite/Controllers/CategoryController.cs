using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
using pkuBite.DTO;
using pkuBite.Interfaces;
using pkuBite.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pkuBite.Controllers
{
    [ApiController]
    [Route("api/Category")]
    public class CategoryController : ControllerBase
    {
        //private readonly DataContext _db;
        private readonly ICategory _categoryRepository;
        private readonly IFeatures<Category> _featuresRepository;

        //public CategoryController(DataContext db, ICategory categoryRepository)
        public CategoryController(ICategory categoryRepository, IFeatures<Category> featuresRepository)

        {
            //_db = db;
            _categoryRepository = categoryRepository;
            _featuresRepository = featuresRepository;
        }

        // --- GET ALL CATEGORIES --- //
       
        [HttpGet]
        public IActionResult GetCategories(
            [FromQuery] string? filter,
            [FromQuery] string? sort,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize= 5)
        {
          
            IQueryable<Category> categories = _categoryRepository.GetAllCategories();

            categories =  _featuresRepository.Filter(categories, filter);
            categories = _featuresRepository.Sort(categories, sort);
            categories = _featuresRepository.Pagination(categories, pageNo, pageSize);

            var response = new
            {
                StatusCode = 201,
                Message = "Success",
                Data = categories
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
            //return _db.Categories;
        }

        // --- CREATE NEW CATEGORIES --- //
        [Authorize]
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {

            if (category == null)
            {
                return BadRequest("Body cant be null");
            }

            var saved = _categoryRepository.CreateCategory(category);

            var response = new
            {
                StatusCode = 201,
                Message = "Food Item Added",
                saved,
                Data = category
            };

             
            //_db.Categories.Add(category);
            //_db.SaveChanges();

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}

