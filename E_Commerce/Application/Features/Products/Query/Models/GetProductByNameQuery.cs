using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Products.Query.Models
{
    public class GetProductByNameQuery : IRequest<Response<List<GetProductListResponse>>>
    {
        public string Name { get; set; }
        public GetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
