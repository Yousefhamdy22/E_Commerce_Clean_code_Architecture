using E_Commerce.Application.Dtos;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using E_Commerce.Services.Abstraction.OrderService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services.Implemantation.OrderService
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IOrder _orderRepository;
        private readonly ECommerceContext _context;
        #endregion

        #region Constructor
        public OrderService(UserManager<User> userManager, IOrder orderRepository, ECommerceContext context)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _context = context;
        }

        public Task<string> CancelOrder(int userId, string orderN)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region HandleFunctions
        //public async Task<string> CancelOrder(int userId, string orderN)
        //{
        //    var user = await _userManager.FindByIdAsync(userId.ToString());
        //    if (user == null)
        //    {
        //        return "Invalid user.";
        //    }

        //    var orderDB = await _orderRepository.GetTableNoTracking()
        //        .Include(o => o.Product)
        //        .FirstOrDefaultAsync(o => o.Order.OrderNumber == orderN && o.Order.UserId == userId);

        //    if (orderDB == null)
        //        return "The order number is invalid.";

        //    if (DateTime.Now >= orderDB.Order.OrderDate.Date.AddDays(3))
        //        return "Sorry, you cannot cancel the order after 3 days.";
        //    for (int index = 0; index < orderDB.Order.OrderItems.Count; index++)
        //    {
        //        var product = orderDB.Products[index];
        //        product.Stock += orderDB.Product[index];
        //    }
        //    _context.Orders.RemoveRange(orderDB);
        //    await _context.SaveChangesAsync();
        //    return "Order canceled successfully.";
        //}

        public Task<Order> CreateOrder(int userId, OrderDto orderDto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }


}

