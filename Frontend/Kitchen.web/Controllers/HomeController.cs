using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kitchen.web.Models;
using System.Security.Claims;
using Kitchen.web.Models.DTOs;
using Kitchen.web.Services.IServices;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kitchen.web.Controllers;

public class HomeController : Controller
{
    private readonly IUserService _userservice;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IUserService userservice)
    {
        _logger = logger;
        _userservice = userservice;
    }

    public async Task<IActionResult> Index()
    {

        if (HttpContext.Request.Cookies["token"] != null)
        {
            UserDTO user = new();
            var response = await _userservice.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (response == null)
            {
                await Logout();
            }
            else
            {
                if (response is not null & response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    if (user != null)
                    {
                        ViewData["User"] = user;
                        ViewData["UserRole"] = user.Role;
                    }
                }

            }
        }
        return View();
    }
    public async Task<IActionResult> Profile()
    {
        if (HttpContext.Request.Cookies["token"] == null)
        {
            return RedirectToAction("LoginOrRigester");

        }
        UserDTO user = new();
        var response = await _userservice.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
        if (response == null)
        {
            await Logout();
            return RedirectToAction("LoginOrRigester");
        }

        if (response is not null || response.IsSuccess)
        {
            user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
        }
        if (user != null)
        {
            ViewData["User"] = user;
            ViewData["UserRole"] = user.Role;
        }
        return View(user);
    }
    public async Task<IActionResult> LoginOrRigester(string res)
    {
        if (HttpContext.Request.Cookies["token"] != null)
        {
            UserDTO user = new();
            var response = await _userservice.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (response != null)
            {
                return RedirectToAction("Profile");
            }
        }

        string ress = res;
        return View("LoginOrRigester", ress);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
    {
        var Token = await _userservice.LoginUser<ResponseDTO>(loginUserDTO);

        if (Token.StatusCode == 404)
        {
            return await LoginOrRigester(Token.Result.ToString());
        }
        else
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(2),
                IsEssential = true
            };
            HttpContext.Response.Cookies.Append("token", Token.Result.ToString(), cookieOptions);
        }
        return RedirectToAction("Profile");
    }
    [HttpPost]
    public async Task<IActionResult> Rigester(AddUserDTO addUser)
    {
        var Token = await _userservice.RigesterUser<ResponseDTO>(addUser);

        if (Token.StatusCode == 404)
        {
            return await LoginOrRigester(Token.Result.ToString());
        }
        else
        {
            return await LoginOrRigester(Token.Result.ToString());
        }
    }
    public async Task<IActionResult> Logout()
    {
        HttpContext.Response.Cookies.Delete("token");
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> EditProfile(int Id)
    {
        if (HttpContext.Request.Cookies["token"] == null)
        {
            return RedirectToAction("LoginOrRigester");

        }
        UserDTO user = new();
        var response = await _userservice.GetUserbyId<ResponseDTO>(Id, HttpContext.Request.Cookies["token"].ToString());

        if (response is not null || response.IsSuccess)
        {
            user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
        }
        if (user != null)
        {
            ViewData["User"] = user;
            ViewData["UserRole"] = user.Role;
        }

        return View(user);
    }
    public async Task<IActionResult> GetAllUsers()
    {
        IEnumerable<UserDTO> user = null;
        var response = await _userservice.GetAllUser<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

        if (response is not null || response.IsSuccess)
        {
            user = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(Convert.ToString(response.Result));
        }
        return PartialView("_GetAllUser", user);
    }
    public async Task<IActionResult> RemoveUser(int Id)
    {
        var user = _userservice.DeleteUser<ResponseDTO>(Id, HttpContext.Request.Cookies["token"].ToString());
        return RedirectToAction("Users", "Admin");
    }
    public async Task<IActionResult> UpdateRoleUser(int Id, string Role)
    {
        var user = await _userservice.ChangeRole<ResponseDTO>(Id, Role, HttpContext.Request.Cookies["token"].ToString());
        return RedirectToAction("Users", "Admin");

    }
    [HttpPost]
    public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUser)
    {
        var user = await _userservice.UpdateUser<ResponseDTO>(updateUser, HttpContext.Request.Cookies["token"].ToString());
        return RedirectToAction("Profile");

    }
    public async Task SetPushToken(string PushToken)
    {
        var res = await _userservice.SetPushToken<ResponseDTO>(PushToken, HttpContext.Request.Cookies["token"].ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
