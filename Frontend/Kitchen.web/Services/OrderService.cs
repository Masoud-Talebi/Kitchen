using Kitchen.web.Services;

namespace Kitchen.web;

public class OrderService : BaseService, IOrderService
{
    private readonly IHttpClientFactory httpClient;
    public OrderService(IHttpClientFactory httpClient) : base(httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<T> AddOrder<T>(AddOrderDTO addOrder, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Order/CreateOrder",
            Data = addOrder,
            AccessToken = AccessToken
        });
    }

    public async Task<T> CanclOrder<T>(int Id, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Order/CancelOrder?Id=" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<T> CompliteOrder<T>(int Id, OrderStatus orderStatus, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.PUT,
            Url = SD.KitchenApiBase + "/api/Order/ComplitedOrder?Id=" + Id + "&orderStatus=" + orderStatus,
            AccessToken = AccessToken
        });
    }

    public async Task<T> DeleteOrder<T>(int Id, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.DELETE,
            Url = SD.KitchenApiBase + "/api/Order?Id=" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<T> GetAllOrder<T>(string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Order",
            AccessToken = AccessToken
        });
    }

    public async Task<T> GetCustomerOrder<T>(string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Order/GetOrderByCustomer",
            AccessToken = AccessToken
        });
    }

    public async Task<T> GetOrderById<T>(int Id, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Order/GetOrderById" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<T> GetOrderItemByOrderId<T>(int Id, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.GET,
            Url = SD.KitchenApiBase + "/api/Order/GetOrderItemById/" + Id,
            AccessToken = AccessToken
        });
    }

    public async Task<string> PaymentZarinPal(int OrderId, string AccessToken)
    {
        return await this.Send<string>(new ApiRequest
        {
            ApiType = SD.ApiType.POST,
            Url = SD.KitchenApiBase + "/api/Order/PaymentZarinPal?OrderId=" + OrderId,
            AccessToken = AccessToken
        });
    }

    public async Task<T> UpdateOrder<T>(UpdateOrderDTO updateOrder, string AccessToken)
    {
        return await this.Send<T>(new ApiRequest
        {
            ApiType = SD.ApiType.PUT,
            Url = SD.KitchenApiBase + "/api/Order",
            Data = updateOrder,
            AccessToken = AccessToken
        });
    }
}
