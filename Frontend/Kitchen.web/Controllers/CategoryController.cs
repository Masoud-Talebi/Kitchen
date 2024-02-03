using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        // GET: CategoryController
        public async Task<ActionResult> Index(string search)
        {
            IEnumerable<CategoryDTO> Categorys = null;
            var response = await _service.GetAllCategory<ResponseDTO>();
            if (response is not null || response.IsSuccess)
            {
                if (search != null)
                {
                    Categorys = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(response.Result)).Where(p => p.Title.ToLower().Contains(search.ToLower()));
                }
                else
                {
                    Categorys = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(response.Result));
                }

            }
            foreach (var item in Categorys)
            {
                item.Image = SD.KitchenApiBase + item.Image;
                item.svg = SD.KitchenApiBase + item.svg;
            }
            return PartialView("_CategoryAdmin", Categorys);
        }
        public async Task<PartialViewResult> GetAllCategory()
        {
            IEnumerable<CategoryDTO> Categorys = null;
            var response = await _service.GetAllCategory<ResponseDTO>();
            if (response is not null || response.IsSuccess)
            {

                Categorys = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(response.Result));


            }
            foreach (var item in Categorys)
            {
                item.Image = SD.KitchenApiBase + item.Image;
                item.svg = SD.KitchenApiBase + item.svg;
            }
            return PartialView("_Category", Categorys);
        }

    }
}
