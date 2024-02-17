using Kitchen.web.Models.DTOs;
using Kitchen.web.Services.IServices;
using System.Data;

namespace Kitchen.web.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IHttpClientFactory httpClient;
        public UserService(IHttpClientFactory httpClient) : base(httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> DeleteUser<T>(int Id, string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.KitchenApiBase + "/api/User?Id=" + Id,
                AccessToken = AccessToken
            });
        }

        public async Task<T> GetAllUser<T>(string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.KitchenApiBase + "/api/User",
                AccessToken = AccessToken
            });
        }

        public async Task<T> GetUserbyId<T>(int Id, string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.KitchenApiBase + "/api/User/" + Id,
                AccessToken = AccessToken
            });
        }

        public async Task<T> LoginUser<T>(LoginUserDTO LoginUser)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = LoginUser,
                Url = SD.KitchenApiBase + "/api/User/Login",
            });
        }

        public async Task<T> RigesterUser<T>(AddUserDTO addUser)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = addUser,
                Url = SD.KitchenApiBase + "/api/User",
            });
        }

        public async Task<T> UpdateUser<T>(UpdateUserDTO updateUser, string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = updateUser,
                Url = SD.KitchenApiBase + "/api/User",
                AccessToken = AccessToken
            });
        }
        public async Task<T> ChangeRole<T>(int Id, string role, string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = SD.KitchenApiBase + "/api/User/ChangeRole?Id=" + Id + "&role=" + role,
                AccessToken = AccessToken
            });
        }
        public async Task<T> GetUserLogined<T>(string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.KitchenApiBase + "/api/User/GetUserLogined",
                AccessToken = AccessToken
            });
        }
        public async Task<T> SetPushToken<T>(string Pushtoken, string AccessToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = SD.KitchenApiBase + "/api/User/SetPushToken?Pushtoken=" + Pushtoken,
                AccessToken = AccessToken
            });
        }

    }
}