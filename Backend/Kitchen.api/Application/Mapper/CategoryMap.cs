using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api;

public class CategoryMap : Profile
{
    public CategoryMap()
    {
        CreateMap<Category,CategoryDTO>().ReverseMap();
       CreateMap<AddCategoryDTO, Category>();
        CreateMap<UpdateCategoryDTO, Category>();
    }

}
