using E_Commerce.Application.Base;
using E_Commerce.Domain.Dtos;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {
    }
}
