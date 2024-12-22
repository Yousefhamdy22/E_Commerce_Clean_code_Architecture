using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.OrdersCommand.Models
{
    public class DeleteOrderCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public DeleteOrderCommand(int Userid, string name)
        {
            UserId = Userid;
            OrderNumber = name;
        }
    }
}
