using Kitchen.web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IFoodService _foodService;
        public AdminController(IUserService userService, ICategoryService categoryService, IFoodService foodService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _foodService = foodService;
        }
        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role == "Admin")
            {
                return View();
            }
            else
            {
                return View("404");
            }

        }
        public async Task<IActionResult> Categorys()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> Foods()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> Orders()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> Users()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> Comments()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> OrderForm(int OrderId)
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            ViewData["OrderId"] = OrderId;
            return View();
        }
        public async Task<IActionResult> Settings()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> AddCategory()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> UpdateCategory(int Id)
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            CategoryDTO category = new();
            var res = await _categoryService.GetCategorybyID<ResponseDTO>(Id);

            if (res is not null || res.IsSuccess)
            {
                category = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(res.Result));
                ViewData["AdminUser"] = user.FullName;
            }

            category.Image = SD.KitchenApiBase + category.Image;
            category.svg = SD.KitchenApiBase + category.svg;
            return View(category);
        }
        public async Task<IActionResult> AddFood()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            List<CategoryDTO> list = new();
            var catres = await _categoryService.GetAllCategory<ResponseDTO>();
            if (catres is not null || catres.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(catres.Result));
            }
            ViewData["Category"] = list;
            if (user.Role != "Admin")
            {
                return View("404");
            }
            return View();
        }
        public async Task<IActionResult> UpdateFood(int Id)
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return View("404");
            }
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            if (user.Role != "Admin")
            {
                return View("404");
            }
            FoodDTO Food = new();
            var res = await _foodService.GetFoodbyID<ResponseDTO>(Id);

            if (res is not null || res.IsSuccess)
            {
                Food = JsonConvert.DeserializeObject<FoodDTO>(Convert.ToString(res.Result));
                ViewData["AdminUser"] = user.FullName;
            }
            

            Food.Image = SD.KitchenApiBase + Food.Image;
            return View(Food);
        }

    }
}
