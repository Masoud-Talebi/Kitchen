using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api;

public interface IUserService
{
    public Task<IEnumerable<UserDTO>> GetAllUser();
    public Task<UserDTO> GetUserById(int Id);
    public Task<UserDTO> Rigester(AddUserDTO addUserDTO);
    public Task<UserDTO> UpdateUser(UpdateUserDTO UpdateUserDTO);
    public Task<string> LoginUser(User user);
    public Task<bool> DeleteUser(int Id);
    Task<bool> ChangeRole(int Id, string role);
}
