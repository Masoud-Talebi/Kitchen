using Kitchen.api;
using Kitchen.api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        public UserController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            response = new ResponseDTO();
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<object> GetAllUser()
        {

            try
            {
                var user = await _userService.GetAllUser();
                response.Result = user;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;

        }
        [Authorize()]
        [HttpGet("{Id}")]
        public async Task<object> GetUserById(int Id)
        {

            try
            {
                var user = await _userService.GetUserById(Id);
                response.Result = user;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;

        }
        [HttpPost]
        public async Task<object> Rigester(AddUserDTO addUserDTO)
        {
            try
            {

                var userold = await _context.Users.Where(p => p.deleted == false).FirstOrDefaultAsync(p => p.Phone == addUserDTO.Phone);
                if (userold == null)
                {
                    var user = await _userService.Rigester(addUserDTO);
                    response.Result = "اکانت شما با موفقیت ثبت شد لطفا لاگین کنید";
                }
                else
                {
                    response.Result = "کاربری با این شماره مبایل وجود دارد";
                    response.StatusCode = 404;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost("Login")]
        public async Task<object> Login(LoginUserDTO loginUserDTO)
        {

            try
            {
                var user = await _context.Users.Where(p => p.deleted == false).FirstOrDefaultAsync(p => p.Phone == loginUserDTO.Phone);
                if (user == null)
                {
                    response.Result = "کاربری با این موبایل یافت نشد";
                    response.StatusCode = 404;
                }
                else if (user.Password != loginUserDTO.Password)
                {
                    response.Result = "رمز عبور اشتباه است";
                    response.StatusCode = 404;
                }
                else
                {
                    var Token = await _userService.LoginUser(user);
                    response.Result = Token;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;

        }
        [Authorize]
        [HttpPut]
        public async Task<object> UpdateUser(UpdateUserDTO UpdateUserDTO)
        {
            try
            {
                var user = await _userService.UpdateUser(UpdateUserDTO);
                response.Result = user;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<object> DeleteUser(int Id)
        {
            try
            {
                var user = await _userService.DeleteUser(Id);
                response.Result = user;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("ChangeRole")]
        public async Task<object> ChangeRole(int Id, string role)
        {
            try
            {
                var user = await _userService.ChangeRole(Id, role);
                response.Result = user;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize]
        [HttpGet("GetUserLogined")]
        public async Task<object> GetUserLogined()
        {
            try
            {
                var user = await _userService.GetUserById(int.Parse(User.Identity.Name));
                response.Result = user;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize]
        [HttpPost("SetPushToken")]
        public async Task<object> SetPushToken(string Pushtoken)
        {
            try
            {
                var user =await _context.Users.FirstOrDefaultAsync(p=>p.Id == int.Parse(User.Identity.Name));
                user.PushNotifacation = Pushtoken;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                response.IsSuccess = true;
                response.Result = " ";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
    }

}
