using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api.Application.Mapper
{
    public class OrderItemMap : Profile
    {
        public OrderItemMap()
        {
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<AddOrderItemDTO, OrderItem>();
            CreateMap<UpdateOrderItemDTO, OrderItem>();
        }
    }
}