using E_Commerce.Application.Base;
using E_Commerce.Domain.Dtos;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Query.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }


    }
}
