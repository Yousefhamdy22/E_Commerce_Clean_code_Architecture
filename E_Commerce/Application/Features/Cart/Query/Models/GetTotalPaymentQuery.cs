using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Cart.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Query.Models
{
    public class GetTotalPaymentQuery : IRequest<Response<GetTotalPaymentResponse>>
    {
        public int UserId { get; set; }

        public GetTotalPaymentQuery(int userId)
        {
            UserId = userId;
        }
    }
}
