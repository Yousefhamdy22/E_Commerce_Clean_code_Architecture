using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Query.Result
{
    public class GetCategoryByNameQuery : IRequest<Response<List<GetCategoryListResponse>>>
    {
        public string Name { get; set; }
        public GetCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }
}
