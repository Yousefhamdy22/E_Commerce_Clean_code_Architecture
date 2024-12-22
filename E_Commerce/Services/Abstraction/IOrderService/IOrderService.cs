using E_Commerce.Application.Dtos;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Abstraction.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(int userId , OrderDto orderDto);
      //  Task<IEnumerable<Order>> GetUserOrders(int userId);
      //Task<string> CheckOut(int userId);
     // Task<List<Order>> ViewOrders(int userId);
        Task<string> CancelOrder(int userId, string orderN);
     // Task<string> TrackOrder(int userId, string orderN);
  

    }
}
