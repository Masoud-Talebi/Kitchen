namespace Kitchen.web;

public interface IFoodService
{
    Task<T> GetAllFood<T>();
    Task<T> GetFoodbyID<T>(int Id);
    Task<T> AddFood<T>(AddFoodDTO AddFood, string AccessToken);
    Task<T> UpdateFood<T>(UpdateFoodDTO updateFood, string AccessToken);
    Task<T> RemoveFood<T>(int Id, string AccessToken);
    Task<T> AddFoodCategory<T>(List<int> CategoryId, int FoodId, string AccessToken);
    Task<T> DeleteFoodCategory<T>(int CategoryId, int FoodId, string AccessToken);
}
