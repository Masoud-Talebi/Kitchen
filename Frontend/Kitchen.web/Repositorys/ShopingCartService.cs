
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Kitchen.web;

public class ShopingCartService : IShopingCartService
{
    private readonly IDistributedCache _redisCache;
    public ShopingCartService(IDistributedCache redisCache)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
    }

    public async Task AddQuantity(int UserId, int FoodId)
    {
        var cart = await GetAllShopingCart(UserId);
        await _redisCache.RemoveAsync(UserId.ToString());
        var food = cart.Items.FirstOrDefault(p => p.FoodId == FoodId);
        cart.Items.Remove(food);
        food.Quantity += 1;
        cart.Items.Add(food);

        await _redisCache.SetStringAsync(UserId.ToString(), JsonConvert.SerializeObject(cart));
    }

    public async Task<ShopingCart> GetAllShopingCart(int UserId)
    {
        var basket = await _redisCache.GetStringAsync(UserId.ToString());
        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }
        return JsonConvert.DeserializeObject<ShopingCart>(basket);
    }

    public async Task Remove(int UserId, int foodId)
    {
        var cart = await GetAllShopingCart(UserId);
        await _redisCache.RemoveAsync(UserId.ToString());
        var food = cart.Items.FirstOrDefault(p => p.FoodId == foodId);
        cart.Items.Remove(food);

        await _redisCache.SetStringAsync(UserId.ToString(), JsonConvert.SerializeObject(cart));
    }

    public async Task RemoveQuantity(int UserId, int FoodId)
    {
        var cart = await GetAllShopingCart(UserId);
        await _redisCache.RemoveAsync(UserId.ToString());
        var food = cart.Items.FirstOrDefault(p => p.FoodId == FoodId);
        cart.Items.Remove(food);
        food.Quantity -= 1;
        if (food.Quantity > 0)
        {
            cart.Items.Add(food);
        }


        await _redisCache.SetStringAsync(UserId.ToString(), JsonConvert.SerializeObject(cart));
    }

    public async Task<int> UpdateShopingCart(ShopingCart shopingCart)
    {
        var cart1 = await GetAllShopingCart(shopingCart.UserId);
        await _redisCache.RemoveAsync(shopingCart.UserId.ToString());
        if (cart1 != null)
        {
            if (cart1.Items.Count() != 0)
            {
                bool add = true;
                var count = 0;
                foreach (var item in cart1.Items)
                {
                    foreach (var item2 in shopingCart.Items)
                    {
                        
                        if (item.FoodId == item2.FoodId)
                        {
                            item.Quantity += item2.Quantity;
                            add = false;
                        }
                        count++;
                        if (add == true && count == cart1.Items.Count())
                        {
                            cart1.Items.Add(item2);
                            await _redisCache.SetStringAsync(shopingCart.UserId.ToString(), JsonConvert.SerializeObject(cart1));
                            break;
                        }
                    }   


                }
                if(add == false)
                {
                    await _redisCache.SetStringAsync(shopingCart.UserId.ToString(), JsonConvert.SerializeObject(cart1));
                }
                

            }
            else
            {
                await _redisCache.SetStringAsync(shopingCart.UserId.ToString(), JsonConvert.SerializeObject(shopingCart));
            }

        }
        else
        {
            await _redisCache.SetStringAsync(shopingCart.UserId.ToString(), JsonConvert.SerializeObject(shopingCart));
        }
        var cart2 = await GetAllShopingCart(shopingCart.UserId);
        var foodquantity = cart2.Items.FirstOrDefault(p => p.FoodId == shopingCart.Items.FirstOrDefault().FoodId);
        return foodquantity.Quantity;
    }
}
