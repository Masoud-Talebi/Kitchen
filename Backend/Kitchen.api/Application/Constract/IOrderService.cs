using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAllOrder();
    Task<OrderDTO> GetOrderById(int Id);
    Task<OrderDTO> AddOrder(AddOrderDTO addOrder);
    Task<OrderDTO> UpdateOrder(UpdateOrderDTO updateOrder);
    Task<IEnumerable<OrderDTO>> GetUserOrder(int UserId);
    Task<bool> DeleteOrder(int Id);
    Task<OrderDTO> PayOrder(int Id);
    Task<OrderDTO> CanclOrder(int Id, int UserId);
    Task<List<OrderItemDTO>> GetOrderItemById(int Id);
    Task<OrderDTO> CompliteOrder(int Id, OrderStatus orderStatus);
}
