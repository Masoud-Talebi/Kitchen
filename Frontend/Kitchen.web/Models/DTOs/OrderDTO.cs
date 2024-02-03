using System.ComponentModel.DataAnnotations;

namespace Kitchen.web;

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
public enum StatePay
{
    IsPay = 1,
    NotPay = 2,
}
public enum OrderStatus
{
    Complited = 1,
    Pending = 2,
    Failed = 3,
    Sending = 4,
}
