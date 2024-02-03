using Kitchen.api.Application.DTOs;

namespace Kitchen.api.Application.Constract
{
    public interface ISettingService
    {
        Task<SettingDTO> GetSetting();
        Task<SettingDTO> UpdateSetting(SettingDTO settingdto);

    }
}
