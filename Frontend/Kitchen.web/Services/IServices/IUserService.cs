using Kitchen.web.Models.DTOs;

namespace Kitchen.web.Services.IServices
{
    public interface IUserService
    {
        Task<T> GetAllUser<T>(string AccessToken);
        Task<T> GetUserbyId<T>(int Id, string AccessToken);
        Task<T> DeleteUser<T>(int Id, string AccessToken);
        Task<T> RigesterUser<T>(AddUserDTO addUser);
        Task<T> LoginUser<T>(LoginUserDTO LoginUser);
        Task<T> UpdateUser<T>(UpdateUserDTO updateUser, string AccessToken);
        Task<T> ChangeRole<T>(int Id, string role,string AccessToken);
        Task<T> GetUserLogined<T>(string AccessToken);
    }
}