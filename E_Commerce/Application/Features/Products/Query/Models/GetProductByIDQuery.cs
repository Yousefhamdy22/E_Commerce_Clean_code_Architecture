using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Products.Query.Models
{
    public class GetProductByIDQuery : IRequest<Response<GetSingleProductResponse>>
    {
        public int Id { get; set; }
        public GetProductByIDQuery(int id)
        {
            Id = id;
        }
    }
}
