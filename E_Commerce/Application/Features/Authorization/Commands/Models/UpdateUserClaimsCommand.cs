using E_Commerce.Application.Base;
using E_Commerce.Domain.Request;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsRequests, IRequest<Response<string>>
    {
    }
}
