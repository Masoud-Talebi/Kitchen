using Kitchen.api;
using Kitchen.api.Application.Constract;
using Kitchen.api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly ICategoryService _categoryservice;
        private readonly SqlserverApplicationContext _context;
        public CategoryController(ICategoryService categoryservice, SqlserverApplicationContext context)
        {
            _categoryservice = categoryservice;
            response = new ResponseDTO();
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<object> GetAllCategory()
        {
            try
            {
                var Categorys = await _categoryservice.GetAllCategory();
                response.Result = Categorys;
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
        public async Task<object> GetCategoryById(int Id)
        {
            try
            {
                var Category = await _categoryservice.GetCategorybyID(Id);
                response.Result = Category;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost]
        public async Task<object> CreateCategory(AddCategoryDTO addCategory)
        {
            try
            {
                var Category = await _categoryservice.AddCategory(addCategory);
                response.Result = Category;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPut]
        public async Task<object> UpdateCategory(UpdateCategoryDTO updateCategory)
        {
            try
            {
                var Category = await _categoryservice.UpdateCategory(updateCategory);
                response.Result = Category;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpDelete]
        public async Task<object> RemoveCategory(int Id)
        {
            try
            {
                var Category = await _categoryservice.RemoveCategory(Id);
                response.Result = Category;
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
