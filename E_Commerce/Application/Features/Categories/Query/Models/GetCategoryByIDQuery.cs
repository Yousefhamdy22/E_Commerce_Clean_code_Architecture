using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Categories.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Query.Models
{
    public class GetCategoryByIDQuery : IRequest<Response<GetSingleCategoryResponse>>
    {
        public int Id { get; set; }
        public GetCategoryByIDQuery(int id)
        {
            Id = id;
        }
    }
}
