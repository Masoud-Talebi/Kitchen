using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;
namespace Kitchen.api;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<User,UserDTO>().ReverseMap();
       CreateMap<AddUserDTO, User>();
        CreateMap<UpdateUserDTO, User>();
    }
}
