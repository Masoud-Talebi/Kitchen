using Kitchen.api.Application.DTOs;

namespace Kitchen.api;

public interface IFoodService
{
    Task<IEnumerable<FoodDTO>> GetAllFood();
    Task<FoodDTO> GetFoodbyID(int Id);
    Task<FoodDTO> AddFood(AddFoodDTO AddFood);
    Task<FoodDTO> UpdateFood(UpdateFoodDTO updateFood);
    Task<bool> RemoveFood(int Id);
    Task AddFoodCategory(List<int> CategoryId, int FoodId);
    Task DeleteFoodCategory(int CategoryId, int FoodId);
}
