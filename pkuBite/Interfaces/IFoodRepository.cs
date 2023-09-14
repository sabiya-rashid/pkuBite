using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface IFoodRepository
    {
        ICollection<FoodItem> GetFoodItems();
        FoodItem GetFood(int id);
        bool FoodExists(int id);
        bool Delete(int id);
        bool UpdateFoodItem(FoodItem foodItem);
        bool Save();
    }
}
