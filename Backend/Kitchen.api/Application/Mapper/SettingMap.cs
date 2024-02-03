using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api;

public class SettingMap:Profile
{
    public SettingMap()
    {
        CreateMap<Setting, SettingDTO>().ReverseMap();
    }
}
