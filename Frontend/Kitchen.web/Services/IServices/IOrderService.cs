namespace Kitchen.web;

public interface IOrderService
{
    Task<T> GetAllOrder<T>(string AccessToken);
    Task<T> GetOrderById<T>(int Id, string AccessToken);
    Task<T> AddOrder<T>(AddOrderDTO addOrder, string AccessToken);
    Task<T> UpdateOrder<T>(UpdateOrderDTO updateOrder, string AccessToken);
    Task<T> GetCustomerOrder<T>(string AccessToken);
    Task<T> DeleteOrder<T>(int Id, string AccessToken);
    Task<T> CanclOrder<T>(int Id, string AccessToken);
    Task<T> GetOrderItemByOrderId<T>(int Id, string AccessToken);
    Task<T> CompliteOrder<T>(int Id, OrderStatus orderStatus, string AccessToken);
    Task<string> PaymentZarinPal(int OrderId, string AccessToken);
}
