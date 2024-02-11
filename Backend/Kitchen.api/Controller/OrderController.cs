using Kitchen.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Kitchen.api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly IOrderService _orderService;
        private readonly ApplicationDbContext _context;
        public OrderController(IOrderService orderervice, ApplicationDbContext context)
        {
            _orderService = orderervice;
            response = new ResponseDTO();
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<object> GetAllOrders()
        {
            try
            {
                var Orders = await _orderService.GetAllOrder();
                response.Result = Orders;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpGet("GetOrderById{Id}")]
        public async Task<object> GetOrderById(int Id)
        {
            try
            {
                var Orders = await _orderService.GetOrderById(Id);
                response.Result = Orders;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpGet("GetOrderByCustomer")]
        public async Task<object> GetOrderByCustomer()
        {
            try
            {
                var Orders = await _orderService.GetUserOrder(int.Parse(User.Identity.Name));
                response.Result = Orders;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpGet("GetOrderItemById/{Id}")]
        public async Task<object> GetOrderItemById(int Id)
        {
            try
            {
                var OrderItems = await _orderService.GetOrderItemById(Id);
                response.Result = OrderItems;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost("CreateOrder")]
        public async Task<object> CreateOrder(AddOrderDTO addOrder)
        {
            try
            {
                addOrder.UserId = int.Parse(User.Identity.Name);
                var OrderItems = await _orderService.AddOrder(addOrder);
                response.Result = OrderItems;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPut]
        public async Task<object> UpdateOrder(UpdateOrderDTO updateOrder)
        {
            try
            {
                updateOrder.UserId = int.Parse(User.Identity.Name);
                var OrderItems = await _orderService.UpdateOrder(updateOrder);
                response.Result = OrderItems;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("ComplitedOrder")]
        public async Task<object> ComplitedOrder(int Id,OrderStatus orderStatus)
        {
            try
            {
                var OrderItems = await _orderService.CompliteOrder(Id,orderStatus);
                response.Result = OrderItems;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost("PaymentZarinPal")]
        public async Task<IActionResult> PaymentZarinPal(int OrderId)
        {
            var user = await _context.Users.FindAsync(int.Parse(User.Identity.Name));
            var order = await _orderService.GetOrderById(OrderId);
            int total = int.Parse(order.TotalPrice.ToString());
            var pyment = new ZarinpalSandbox.Payment(total);
            var res = pyment.PaymentRequest($"پرداخت فاکتور شمارهی {order.Id}", "https://localhost:7250/api/Order/PaymentOrder/" + order.Id, user.Email, user.Phone);
            if (res != null && res.Result != null)
            {
                if (res.Result.Status == 100)
                {
                    return Ok("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);

                }
                else
                {
                    return NotFound("امکان اتصال به درگاه بانکی وجود ندارد");
                }
            }
            return NotFound("امکان اتصال به درگاه بانکی وجود ندارد");
        }
        [AllowAnonymous]
        [HttpGet("PaymentOrder/{Id}")]
        public async Task<object> PaymentOrder(int Id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string Authority = HttpContext.Request.Query["Authority"].ToString();
                var order = await _orderService.GetOrderById(Id);
                int total = int.Parse(order.TotalPrice.ToString());
                var pay = new ZarinpalSandbox.Payment(total);
                var res = pay.Verification(Authority).Result;
                if (res.Status == 100)
                {
                    try
                    {
                        var OrderItems = await _orderService.PayOrder(Id);
                        response.Result = OrderItems;
                        return Redirect("http://localhost:5235/Order/OrderRes?OrderId=" + Id);
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = new List<string> { ex.Message };
                    }
                    return Redirect("http://localhost:5235/Order/OrderRes?OrderId=" + Id);
                }
                else
                {
                    return Redirect("http://localhost:5235/Order/OrderRes?OrderId=" + Id);
                }
                
            }
            return Redirect("http://localhost:5235/Order/OrderRes?OrderId=" + Id);


        }
            [HttpPost("CancelOrder")]
            public async Task<object> CancelOrder(int Id)
            {
                try
                {
                    var OrderItems = await _orderService.CanclOrder(Id, int.Parse(User.Identity.Name));
                    response.Result = OrderItems;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string> { ex.Message };
                }
                return response;
            }
            [Authorize(Roles = "Admin")]
            [HttpDelete]
            public async Task<object> DeleteOrder(int Id)
            {
                try
                {
                    var OrderItems = await _orderService.DeleteOrder(Id);
                    response.Result = OrderItems;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string> { ex.Message };
                }
                return response;
            }
        }
    }
