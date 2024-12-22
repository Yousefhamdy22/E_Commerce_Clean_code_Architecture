using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Cart.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Query.Models
{
    public class GetCartsByUserIdQuery : IRequest<Response<GetCartsResponse>>
    {
        public int UserId { get; set; }
        public GetCartsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
