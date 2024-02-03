using Kitchen.web.Services;

namespace Kitchen.web;

public class FoodService : BaseService, IFoodService
{
    private readonly IHttpClientFactory httpClient;
    public FoodService(IHttpClientFactory httpClient) : base(httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<T> AddFood<T>(AddFoodDTO AddFood, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Food",
            Data = AddFood,
            AccessToken = AccessToken
        });
    }

    public async Task<T> AddFoodCategory<T>(List<int> CategoryId, int FoodId, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Food/AddFoodCategory?FoodId=" + FoodId,
            Data = CategoryId,
            AccessToken = AccessToken
        });
    }

    public async Task<T> DeleteFoodCategory<T>(int CategoryId, int FoodId, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Food/RemoveFoodCategory/" + FoodId + "/" + CategoryId,
            AccessToken = AccessToken
        });
    }

    public async Task<T> GetAllFood<T>()
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Food",

        });
    }

    public async Task<T> GetFoodbyID<T>(int Id)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Food/" + Id,

        });
    }

    public async Task<T> RemoveFood<T>(int Id, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Food?Id=" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<T> UpdateFood<T>(UpdateFoodDTO updateFood, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Food",
            Data = updateFood,
            AccessToken = AccessToken
        });
    }
}
