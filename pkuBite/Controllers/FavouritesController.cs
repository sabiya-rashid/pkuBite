using DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pkuBite.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : Controller
    {
        private readonly DataContext _context;
        public FavouritesController(DataContext context)
        {
            _context = context;      
        }
        [HttpGet]
        public IActionResult GetFavorites(int userId)
        {
            var user = _context.Users.Include(u => u.Favourites).ThenInclude(f => f.Food).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var favoriteFoods = user.Favourites.Select(f => f.Food).ToList();
            return Ok(favoriteFoods);
        }
        [HttpPost("{foodId}")]
        public IActionResult AddFavorite(int userId, int foodId)
        {
            var user = _context.Users.Include(u => u.Favourites).FirstOrDefault(u => u.Id == userId);
            var food = _context.Foods.Find(foodId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (food == null)
            {
                return NotFound("Food not found");
            }
            if (user.Favourites == null)
            {
                user.Favourites = new List<Favourites>();
            }

            if (!user.Favourites.Any(f => f.FoodId == foodId))
            {
                var favorite = new Favourites
                {
                    UserId = userId,
                    FoodId = foodId
                };

                _context.Favourites.Add(favorite);
                _context.SaveChanges();
            }

            return Ok(user.Favourites.Select(f => f.Food).ToList());
        }
        [HttpDelete("{foodId}")]
        public IActionResult RemoveFavorite(int userId, int foodId)
        {
            var user = _context.Users.Include(u => u.Favourites).FirstOrDefault(u => u.Id == userId);
            var favorite = user?.Favourites.FirstOrDefault(f => f.FoodId == foodId);

            if (user == null || favorite == null)
            {
                return NotFound("Favorite not found");
            }

            _context.Favourites.Remove(favorite);
            _context.SaveChanges();

            return Ok(user.Favourites.Select(f => f.Food).ToList());
        }
    }
}
