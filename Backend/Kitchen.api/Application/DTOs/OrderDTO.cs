using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api;

public class OrderDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserPhone { get; set; }
    public string Adress { get; set; }
    public double TotalPrice { get; set; }
    public StatePay StatePay { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PersianCreated { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}
public class AddOrderDTO
{
    public int UserId { get; set; }
    public string Adress { get; set; }
    public List<AddOrderItemDTO> AddOrderItemDTOs { get; set; }
}
public class UpdateOrderDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Adress { get; set; }
    public List<UpdateOrderItemDTO> OrderItemDTOs { get; set; }
}

