using Kitchen.web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class OrderController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IShopingCartService _shopingCartService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService, IUserService userService, IShopingCartService shopingCartService, ISettingService settingService)
        {
            _orderService = orderService;
            _userService = userService;
            _shopingCartService = shopingCartService;
            _settingService = settingService;
        }
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            IEnumerable<OrderDTO> Orders = null;
            var response = await _orderService.GetAllOrder<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (response is not null || response.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(Convert.ToString(response.Result));
            }
            return PartialView("_OrderAdmin", Orders);
        }
        public async Task<IActionResult> Orders()
        {
            IEnumerable<OrderDTO> Orders = null;
            var response = await _orderService.GetAllOrder<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (response is not null || response.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(Convert.ToString(response.Result)).Where(p => p.StatePay == StatePay.IsPay && p.OrderStatus == OrderStatus.Pending);
            }

            return PartialView("_Orders", Orders);
        }
        public async Task<IActionResult> GetMyOrder()
        {
            IEnumerable<OrderDTO> Orders = null;
            var response = await _orderService.GetCustomerOrder<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (response is not null || response.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(Convert.ToString(response.Result));
            }

            return PartialView("_MyOrders", Orders);
        }
        public async Task<IActionResult> CheckoutOrder()
        {

            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["User"] = user.FullName;
            }


            SettingDTO setting = new();
            var settingres = await _settingService.GetSetting<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (settingres is not null || settingres.IsSuccess)
            {
                setting = JsonConvert.DeserializeObject<SettingDTO>(Convert.ToString(settingres.Result));
                ViewData["PriceForSending"] = setting.PriceForSending;
            }

            var shopingCart = await _shopingCartService.GetAllShopingCart(user.Id);
            if (user.Address != null)
            {
                shopingCart.Adress = user.Address;
            }

            return View(shopingCart);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(string Adress)
        {
            UserDTO user = new();
            var response = await _userService.GetUserLogined<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());

            if (response is not null || response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                ViewData["User"] = user.FullName;
            }
            var shopingCart = await _shopingCartService.GetAllShopingCart(user.Id);
            List<AddOrderItemDTO> addOrderItems = new List<AddOrderItemDTO>();
            foreach (var item in shopingCart.Items)
            {
                AddOrderItemDTO addOrderItem = new AddOrderItemDTO
                {
                    FoodId = item.FoodId,
                    FoodTitle = item.FoodTitle,
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
                addOrderItems.Add(addOrderItem);
            }
            AddOrderDTO addOrderDTO = new AddOrderDTO
            {
                UserId = 5,
                AddOrderItemDTOs = addOrderItems,
                Adress = Adress
            };

            OrderDTO Orders = null;

            var res = await _orderService.AddOrder<ResponseDTO>(addOrderDTO, HttpContext.Request.Cookies["token"].ToString());
            if (res is not null || res.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(res.Result));
            }
            return RedirectToAction("PayOrder", new
            {
                OrderId = Orders.Id
            });
        }
        [HttpPost]
        public async Task<IActionResult> CompliteOrder(int Id, OrderStatus Orderstatus)
        {
            OrderDTO Orders = null;
            var res = await _orderService.CompliteOrder<ResponseDTO>(Id, Orderstatus, HttpContext.Request.Cookies["token"].ToString());
            if (res is not null || res.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(res.Result));
            }
            return RedirectToAction("Index","Admin");
        }
        public async Task<IActionResult> PayOrder(int OrderId)
        {
            var res = await _orderService.PaymentZarinPal(OrderId, HttpContext.Request.Cookies["token"].ToString());
            return Redirect(res);
        }
        public async Task<IActionResult> OrderRes(int OrderId)
        {
            OrderDTO Orders = null;
            var res = await _orderService.GetOrderById<ResponseDTO>(OrderId, HttpContext.Request.Cookies["token"].ToString());
            if (res is not null || res.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(res.Result));
            }
            return View(Orders);
        }
        public async Task<IActionResult> OrderForm(int OrderId)
        {
            OrderDTO Orders = null;
            var res = await _orderService.GetOrderById<ResponseDTO>(OrderId, HttpContext.Request.Cookies["token"].ToString());
            if (res is not null || res.IsSuccess)
            {
                Orders = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(res.Result));
            }
            return PartialView("_OrderById", Orders);
        }

    }
}
