using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Command.Models
{
    public class RemoveCartCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

       
    }
}
