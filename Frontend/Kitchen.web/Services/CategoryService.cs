
using Kitchen.web.Services;

namespace Kitchen.web;

public class CategoryService : BaseService, ICategoryService
{
    private readonly IHttpClientFactory httpClient;
    public CategoryService(IHttpClientFactory httpClient) : base(httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<T> AddCategory<T>(AddCategoryDTO addCategory, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + $"/api/Category?Title={addCategory.Title}&Description={addCategory.Description}",
            Image = addCategory.Image,
            Svg = addCategory.svg,
            AccessToken = AccessToken
        });
    }

    public async Task<T> GetAllCategory<T>()
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Category",
        });
    }

    public async Task<T> GetCategorybyID<T>(int Id)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Category/" + Id,
        });
    }

    public async Task<T> RemoveCategory<T>(int Id, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.DELETE,
            Url = SD.KitchenApiBase + "/api/Category?Id=" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<T> UpdateCategory<T>(UpdateCategoryDTO updateCategory, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.PUT,
            Url = SD.KitchenApiBase + $"/api/Category?Id={updateCategory.Id}&Title={updateCategory.Title}&Description={updateCategory.Description}",
            Image = updateCategory.Image,
            Svg = updateCategory.svg,
            AccessToken = AccessToken
        });
    }
}
