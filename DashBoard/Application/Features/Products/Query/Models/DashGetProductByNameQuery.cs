using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Query.Result;
using MediatR;

namespace DashBoard.Application.Features.Products.Query.Models
{
    public class DashGetProductByNameQuery : IRequest<Response<List<GetProductListResponse>>>
    {
        public string Name { get; set; }
        public DashGetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
