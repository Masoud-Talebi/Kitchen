namespace Kitchen.web;

public interface IShopingCartService
{
    Task<ShopingCart> GetAllShopingCart(int UserId);
    Task<int> UpdateShopingCart(ShopingCart shopingCart);
    Task Remove(int UserId, int foodId);
    Task AddQuantity(int UserId, int FoodId);
    Task RemoveQuantity(int UserId, int FoodId);
}
