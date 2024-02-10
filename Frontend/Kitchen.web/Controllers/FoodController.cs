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
            List<CategoryDTO> list = new();
            var catres = await _categoryservice.GetAllCategory<ResponseDTO>();
            if (catres is not null || catres.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(catres.Result));
            }
            ViewData["Category"] = list;
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
                List<FoodDTO> rfood = new List<FoodDTO>();
                foreach (var item in Food)
                {
                    foreach (var cat in item.Categorys)
                    {
                        if (cat.Id == Id)
                        {
                            rfood.Add(item);
                            break;
                        }
                    }
                }
                return View(rfood);
            }
            return View(Food);

        }
        [HttpPost]
        public async Task<IActionResult> CreateFood([FromForm] AddFoodDTO addFood)
        {
            var response = await _service.AddFood<ResponseDTO>(addFood, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Foods", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFood([FromForm] UpdateFoodDTO updateFood)
        {
            var response = await _service.UpdateFood<ResponseDTO>(updateFood, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Foods", "Admin");
        }
        public async Task<IActionResult> DeleteFood(int Id)
        {
            var response = await _service.RemoveFood<ResponseDTO>(Id, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Foods", "Admin");
        }
        public async Task<IActionResult> DeleteFoodCategory(int CatId, int FoodId)
        {
            var response = await _service.DeleteFoodCategory<ResponseDTO>(CatId, FoodId, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Foods", "Admin");
        }
        public async Task<IActionResult> AddFoodCategory(int CatId, int FoodId)
        {
            List<int> categoryIds = new List<int>();
            categoryIds.Add(CatId);
            var response = await _service.AddFoodCategory<ResponseDTO>(categoryIds, FoodId, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Foods", "Admin");
        }


    }
}
