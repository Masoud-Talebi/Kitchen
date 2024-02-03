
using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Application.Tools;
using Kitchen.api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;

namespace Kitchen.api;

public class OrderService : IOrderService
{
    #region Field
    private readonly SqlserverApplicationContext _context;
    private readonly IMapper _mapper;
    public OrderService(SqlserverApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    #endregion

    public async Task<OrderDTO> AddOrder(AddOrderDTO addOrder)
    {
        Order order = _mapper.Map<Order>(addOrder);
        order.StatePay = StatePay.NotPay;
        order.CreatedDate = DateTime.Now;
        order.OrderStatus = OrderStatus.Pending;
        double totalprice = 0;
        foreach (var item in addOrder.AddOrderItemDTOs)
        {
            totalprice += item.Price * item.Quantity;
        }
        order.TotalPrice = totalprice;
        order.TotalPrice += _context.Settings.FirstOrDefault().PriceForSending;
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        await AddOrderItem(order.Id, addOrder.AddOrderItemDTOs);
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> CanclOrder(int Id, int UserId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(p => p.Id == Id && p.UserId == UserId);
        order.OrderStatus = OrderStatus.Failed;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<bool> DeleteOrder(int Id)
    {
        var order = await _context.Orders.FindAsync(Id);
        order.Deleted = true;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<OrderDTO>> GetAllOrder()
    {
        IEnumerable<Order> orders = await _context.Orders.Include(p => p.User).OrderByDescending(p=>p.CreatedDate).Where(p => p.Deleted == false).ToListAsync();
        IEnumerable<OrderDTO> orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
        foreach (var orderDTO in orderDTOs)
        {
            orderDTO.PersianCreated = orderDTO.CreatedDate.ToPersian();
            List<OrderItemDTO> orderitems = await GetOrderItemById(orderDTO.Id);
            orderDTO.OrderItems = orderitems;
            orderDTO.UserPhone = orders.FirstOrDefault(p=>p.Id == orderDTO.Id).User.Phone;
        }

        return orderDTOs;
    }

    public async Task<OrderDTO> GetOrderById(int Id)
    {
        Order order = await _context.Orders.Include(p=>p.User).FirstOrDefaultAsync(p => p.Id == Id);
        OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);
        orderDTO.PersianCreated = orderDTO.CreatedDate.ToPersian();
        List<OrderItemDTO> orderitems = await GetOrderItemById(orderDTO.Id);
        orderDTO.OrderItems = orderitems;
        orderDTO.UserPhone = order.User.Phone;
        return orderDTO;
    }

    public async Task<IEnumerable<OrderDTO>> GetUserOrder(int UserId)
    {
        IEnumerable<Order> orders = await _context.Orders.Include(p=>p.User).OrderByDescending(p => p.CreatedDate).Where(p => p.Deleted == false && p.UserId == UserId).ToListAsync();
        IEnumerable<OrderDTO> orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
        foreach (var orderDTO in orderDTOs)
        {
            orderDTO.PersianCreated = orderDTO.CreatedDate.ToPersian();
            List<OrderItemDTO> orderitems = await GetOrderItemById(orderDTO.Id);
            orderDTO.OrderItems = orderitems;
            orderDTO.UserPhone = orders.FirstOrDefault().User.Phone;
        }
        return orderDTOs;
    }

    public async Task<OrderDTO> PayOrder(int Id)
    {
        var order = await _context.Orders.FindAsync(Id);
        order.StatePay = StatePay.IsPay;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> CompliteOrder(int Id, OrderStatus orderStatus)
    {
        var order = await _context.Orders.FindAsync(Id);
        order.OrderStatus = orderStatus;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> UpdateOrder(UpdateOrderDTO updateOrder)
    {
        Order order = _mapper.Map<Order>(updateOrder);
        order.StatePay = StatePay.NotPay;
        order.CreatedDate = DateTime.Now;
        order.OrderStatus = OrderStatus.Pending;
        double totalprice = 0;
        foreach (var item in updateOrder.OrderItemDTOs)
        {
            totalprice += item.Price * item.Quantity;
        }
        order.TotalPrice = totalprice;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        await UpdateOrderItem(order.Id, updateOrder.OrderItemDTOs);
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<List<OrderItemDTO>> GetOrderItemById(int Id)
    {
        List<OrderItem> orderItem = await _context.OrderItems.Where(p=>p.OrderId == Id).ToListAsync();
         List<OrderItemDTO> orderItemDTO = _mapper.Map<List<OrderItemDTO>>(orderItem);
        return orderItemDTO;
    }

    #region Options
    public async Task AddOrderItem(int Id, List<AddOrderItemDTO> addorderItemDto)
    {
        foreach (var item in addorderItemDto)
        {
            OrderItem orderItem = _mapper.Map<OrderItem>(item);
            orderItem.OrderId = Id;
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }
    }
    public async Task UpdateOrderItem(int Id, List<UpdateOrderItemDTO> updateorderItemDto)
    {
        foreach (var item in updateorderItemDto)
        {
            OrderItem orderItem = _mapper.Map<OrderItem>(item);
            orderItem.OrderId = Id;
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }
    }


    #endregion

}
