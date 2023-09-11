using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.Dto;
using pkuBite.Interfaces;
using pkuBite.Models;

namespace pkuBite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller 
    {
        private readonly ICommonRepository<Category> _repository;

        public CategoryController(DataContext context, ICommonRepository<Category> repository)
        {
            _repository = repository;
        }
        //[Authorize]
        [HttpGet]
        public IActionResult GetAllCategories(int page, int pageSize)
        {
            int skipCount = (page - 1) * pageSize;

            // Calculate total count for pagination metadata
           var result = _repository.GetEntities();
            List<Category> categories = result.OrderBy(c => c.Id).Skip(skipCount)
                .Take(pageSize).ToList();
            int totalCount = result.Count();
            var response = new
            {
                Success = true,
                Message = "Categories retrieved successfully",
                Data = categories,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = new Category
            {
                Name = categoryDto.Name
            };
            var categoryResult = _repository.CreateEntity(category);         

            return Ok(categoryResult);

        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCategory(int id)
        {
            var category = _repository.EntityExists(id);
            if (category == null)
                return BadRequest(ModelState);
            var result = _repository.GetEntity(id);
            return Ok(category);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = _repository.EntityExists(id);
            if (category == null)
                return NotFound(ModelState);
            category.Name = categoryDto.Name;
            var result = _repository.UpdateEntity(category);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            var category = _repository.EntityExists(id);
            if (category == null)
                return NotFound(ModelState);
            _repository.DeleteEntity(category);
            return Ok("Category deleted");
        }     

    }
}
