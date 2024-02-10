using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class SettingController : Controller
    {
        private readonly ISettingService _service;
        public SettingController(ISettingService settingService)
        {
            _service = settingService;
        }
        // GET: SettingController
        public async Task<ActionResult> Index()
        {
            SettingDTO setting = null;
            var res = await _service.GetSetting<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (res is not null || res.IsSuccess)
            {
                setting = JsonConvert.DeserializeObject<SettingDTO>(Convert.ToString(res.Result));
            }
            return PartialView("_settings", setting);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateSetting(SettingDTO settingDTO)
        {
            SettingDTO setting = null;
            var res = await _service.UpdateSetting<ResponseDTO>(settingDTO, HttpContext.Request.Cookies["token"].ToString());
            if (res is not null || res.IsSuccess)
            {
                setting = JsonConvert.DeserializeObject<SettingDTO>(Convert.ToString(res.Result));
            }
            return RedirectToAction("Settings", "Admin");
        }

    }
}
