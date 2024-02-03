using AutoMapper;
using Kitchen.api.Models;

namespace Kitchen.api;

public class OrderMap : Profile
{
    public OrderMap()
    {
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<AddOrderDTO, Order>();
        CreateMap<UpdateOrderDTO, Order>();
    }

}
