using Kitchen.web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
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

    }
}
