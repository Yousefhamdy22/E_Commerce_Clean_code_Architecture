using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authorization.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Query.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResult>>
    {
        public int Id { get; set; }
        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }

    }
}
