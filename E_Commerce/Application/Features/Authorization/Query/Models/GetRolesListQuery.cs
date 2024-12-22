using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authorization.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Query.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {

    }
}
