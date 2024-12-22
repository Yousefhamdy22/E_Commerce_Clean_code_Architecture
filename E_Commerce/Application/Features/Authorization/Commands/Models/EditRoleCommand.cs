using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
