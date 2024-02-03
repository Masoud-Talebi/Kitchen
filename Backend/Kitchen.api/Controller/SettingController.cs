using Kitchen.api.Application.Constract;
using Kitchen.api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
            response = new ResponseDTO();
        }
        [Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var setting = await _settingService.GetSetting();
                response.Result = setting;
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<object> Update(SettingDTO settingDTO)
        {
            try
            {
                var setting = await _settingService.UpdateSetting(settingDTO);
                response.Result = setting;
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
