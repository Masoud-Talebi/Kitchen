
using Kitchen.web.Services;

namespace Kitchen.web;

public class SettingService : BaseService, ISettingService
{
    private readonly IHttpClientFactory httpClient;
    public SettingService(IHttpClientFactory httpClient) : base(httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<T> GetSetting<T>(string AccesToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Setting",
            AccessToken = AccesToken
        });
    }

    public async Task<T> UpdateSetting<T>(SettingDTO settingdto, string AccesToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.PUT,
            Data = settingdto,
            Url = SD.KitchenApiBase + "/api/Setting",
            AccessToken = AccesToken
        });
    }
}
