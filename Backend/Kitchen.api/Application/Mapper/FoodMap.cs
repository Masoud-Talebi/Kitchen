using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api;

public class FoodMap :Profile
{
    public FoodMap()
    {
        CreateMap<Food,FoodDTO>().ReverseMap();
       CreateMap<AddFoodDTO, Food>();
        CreateMap<UpdateFoodDTO, Food>();
    }

}
