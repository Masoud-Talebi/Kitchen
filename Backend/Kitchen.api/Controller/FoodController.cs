using Kitchen.api;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FoodController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly IFoodService _foodService;
        private readonly SqlserverApplicationContext _context;
        public FoodController(IFoodService foodService, SqlserverApplicationContext context)
        {
            _foodService = foodService;
            response = new ResponseDTO();
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<object> GetAllFood()
        {
            try
            {
                var Foods = await _foodService.GetAllFood();
                response.Result = Foods;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<object> GetFoodById(int Id)
        {
            try
            {
                var Foods = await _foodService.GetFoodbyID(Id);
                response.Result = Foods;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost]
        public async Task<object> CreateFood(AddFoodDTO addFood)
        {
            try
            {
                var food = await _foodService.AddFood(addFood);
                response.Result = food;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPut]
        public async Task<object> UpdateFood(UpdateFoodDTO updateFood)
        {
            try
            {
                var food = await _foodService.UpdateFood(updateFood);
                response.Result = food;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpDelete]
        public async Task<object> RemoveFood(int Id)
        {
            try
            {
                var food = await _foodService.RemoveFood(Id);
                response.Result = food;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost("AddFoodCategory")]
        public async Task<object> AddFoodCategory(int FoodId, List<int> categoryId)
        {
            try
            {
                await _foodService.AddFoodCategory(categoryId,FoodId);
                response.Result = "Success";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpDelete("RemoveFoodCategory/{FoodId}/{categoryId}")]
        public async Task<object> RemoveFoodCategory(int FoodId, int categoryId)
        {
            try
            {
                await _foodService.DeleteFoodCategory(categoryId, FoodId);
                response.Result = "Success";
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
