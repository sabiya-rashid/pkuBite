using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface IFoodRepository
    {
        ICollection<Food> GetFoodItems();
        Food GetFood(int id);
        bool FoodExists(int id);
        bool Delete(int id);
        bool UpdateFoodItem(Food foodItem);
        bool Save();
    }
}
