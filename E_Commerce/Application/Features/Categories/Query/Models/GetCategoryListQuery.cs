using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Categories.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Query.Models
{
    public class GetCategoryListQuery : IRequest<Response<List<GetCategoryListResponse>>>
    {
        // Add any query parameters if needed
    }
}
