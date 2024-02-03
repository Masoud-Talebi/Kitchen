using Kitchen.web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class BasketController : Controller
    {
        private readonly IFoodService _foodservice;
        private readonly IShopingCartService _shopingCartService;
        private readonly IUserService _userService;
        public BasketController(IShopingCartService shopingCartService, IUserService userService, IFoodService foodService)
        {
            _foodservice = foodService;
            _shopingCartService = shopingCartService;
            _userService = userService;
        }
        // GET: BasketController
        public async Task<ActionResult> Index()
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                UserDTO user = new();
                var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

                if (response is not null || response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewData["User"] = user;
                }
                var basket = await _shopingCartService.GetAllShopingCart(user.Id);
                return View(basket ?? new ShopingCart(user.Id));
            }
            return RedirectToRoute("/Home/Index");

        }
        public async Task<IActionResult> SetBasket(int FoodId)
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                UserDTO user = new();
                var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

                if (response is not null || response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewData["User"] = user;
                }
                ShopingCart shoping = new ShopingCart(user.Id);
                FoodDTO food = null;
                var foodres = await _foodservice.GetFoodbyID<ResponseDTO>(FoodId);
                if (response is not null || response.IsSuccess)
                {
                    food = JsonConvert.DeserializeObject<FoodDTO>(Convert.ToString(foodres.Result));
                }
                food.Image = SD.KitchenApiBase + food.Image;
                ShopingCartItem shopingitem = new ShopingCartItem
                {
                    FoodTitle = food.Title,
                    FoodId = food.Id,
                    Image = food.Image,
                    Price = food.Price,
                    Description = food.Description,
                    Quantity = 1,
                };
                shoping.Items.Add(shopingitem);
                shoping.UserId = user.Id;
                var quantity = await _shopingCartService.UpdateShopingCart(shoping);
                return Ok(quantity);
            }
            return Ok("Home/LoginOrRigester");

        }
        public async Task<ActionResult> DeleteBasket(int foodId)
        {

            if (HttpContext.Request.Cookies["token"] != null)
            {
                UserDTO user = new();
                var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

                if (response is not null || response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewData["User"] = user;
                }
                await _shopingCartService.Remove(user.Id, foodId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute("/Home/Index");
        }
        public async Task<IActionResult> AddQuantity(int FoodId)
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                UserDTO user = new();
                var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

                if (response is not null || response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewData["User"] = user;
                }
                await _shopingCartService.AddQuantity(user.Id, FoodId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute("/Home/Index");
        }
        public async Task<IActionResult> DeleteQuantity(int FoodId)
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                UserDTO user = new();
                var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

                if (response is not null || response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewData["User"] = user;
                }
                await _shopingCartService.RemoveQuantity(user.Id, FoodId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute("/Home/Index");
        }

    }
}
