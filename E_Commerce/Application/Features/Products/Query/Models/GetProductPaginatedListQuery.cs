using E_Commerce.Application.Features.Products.Query.Result;
using E_Commerce.Application.Wrapper;
using E_Commerce.Domain.Helpers;
using MediatR;

namespace E_Commerce.Application.Features.Products.Query.Models
{
    public class GetProductPaginatedListQuery : IRequest<PaginatedResult<GetProductPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ProductOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
