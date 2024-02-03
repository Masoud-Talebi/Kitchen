namespace Kitchen.web;

public interface ISettingService
{
    Task<T> GetSetting<T>(string AccesToken);
    Task<T> UpdateSetting<T>(SettingDTO settingdto, string AccesToken);
}
