using Kitchen.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class FoodController : Controller
    {
        private readonly ICategoryService _categoryservice;
        private readonly IFoodService _service;
        public FoodController(IFoodService service, ICategoryService categoryservice)
        {
            _service = service;
            _categoryservice = categoryservice;
        }
        // GET: FoodController
        public async Task<ActionResult> Index(string search)
        {
            IEnumerable<FoodDTO> Categorys = null;
            var response = await _service.GetAllFood<ResponseDTO>();
            if (response is not null || response.IsSuccess)
            {
                Categorys = JsonConvert.DeserializeObject<IEnumerable<FoodDTO>>(Convert.ToString(response.Result));
            }
            foreach (var item in Categorys)
            {
                item.Image = SD.KitchenApiBase + item.Image;
            }
            return PartialView("_FoodAdmin", Categorys);
        }
        public async Task<PartialViewResult> GetAllFood()
        {
            IEnumerable<FoodDTO> Categorys = null;
            var response = await _service.GetAllFood<ResponseDTO>();
            if (response is not null || response.IsSuccess)
            {
                Categorys = JsonConvert.DeserializeObject<IEnumerable<FoodDTO>>(Convert.ToString(response.Result));
            }
            foreach (var item in Categorys)
            {
                item.Image = SD.KitchenApiBase + item.Image;
            }
            return PartialView("_Foods", Categorys);
        }
        public async Task<PartialViewResult> GetAllspecialFood()
        {
            IEnumerable<FoodDTO> Categorys = null;
            var response = await _service.GetAllFood<ResponseDTO>();
            if (response is not null || response.IsSuccess)
            {
                Categorys = JsonConvert.DeserializeObject<IEnumerable<FoodDTO>>(Convert.ToString(response.Result));
            }
            foreach (var item in Categorys)
            {
                item.Image = SD.KitchenApiBase + item.Image;
            }
            return PartialView("_SpecialFoods", Categorys.Where(p => p.Score >= 4));
        }
        public async Task<IActionResult> GetFoodByCategory(int Id)
        {
             ViewData["User"] = HttpContext.Request.Cookies["token"];
            List<CategoryDTO> listcat = new();
            var responsecat = await _categoryservice.GetAllCategory<ResponseDTO>();
            if (responsecat is not null || responsecat.IsSuccess)
            {
                listcat = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(responsecat.Result));
            }
            ViewData["Category"] = listcat;
            IEnumerable<FoodDTO> Food = null;
            var response = await _service.GetAllFood<ResponseDTO>();
            if (response is not null || response.IsSuccess)
            {
                Food = JsonConvert.DeserializeObject<IEnumerable<FoodDTO>>(Convert.ToString(response.Result));
            }
            foreach (var item in Food)
            {
                item.Image = SD.KitchenApiBase + item.Image;
            }
            if (Id != 0)
            {
                return View(Food.Where(p => p.Categorys.Select(p => p.Id == Id).FirstOrDefault()));
            }
            return View(Food);

        }

    }
}
