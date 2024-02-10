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
        string CategoryIds = "";
        foreach (var item in AddFood.CategoryIds)
        {
            CategoryIds += $"&CategoryIds={item}";
        }
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + $"/api/Food?Title={AddFood.Title}&Description={AddFood.Description}&Price={AddFood.Price}&Score={AddFood.Score}{CategoryIds}",
            Image = AddFood.Image,
            AccessToken = AccessToken
        });
    }

    public async Task<T> AddFoodCategory<T>(List<int> CategoryId, int FoodId, string AccessToken)
    {
        string Category = "";
        foreach (var item in CategoryId)
        {
            Category += $"&categoryId={item}";
        }
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + $"/api/Food/AddFoodCategory?FoodId={FoodId}{Category}",
            Data = CategoryId,
            AccessToken = AccessToken
        });
    }

    public async Task<T> DeleteFoodCategory<T>(int CategoryId, int FoodId, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.DELETE,
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
            ApiType = SD.ApiType.DELETE,
            Url = SD.KitchenApiBase + "/api/Food?Id=" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<T> UpdateFood<T>(UpdateFoodDTO updateFood, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.PUT,
            Url = SD.KitchenApiBase + $"/api/Food?Id={updateFood.Id}&Title={updateFood.Title}&Description={updateFood.Description}&Price={updateFood.Price}&Score={updateFood.Score}",
            Image = updateFood.Image,
            AccessToken = AccessToken
        });
    }
}
