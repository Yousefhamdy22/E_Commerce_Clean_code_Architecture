using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
