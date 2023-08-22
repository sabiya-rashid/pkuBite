using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pkuBite.Data;
using pkuBite.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pkuBite.Controllers
{
    [Route("api/Category")]
    public class CategoryController : Controller
    {
       
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return CategoryList.categories;
        }
    }
}

