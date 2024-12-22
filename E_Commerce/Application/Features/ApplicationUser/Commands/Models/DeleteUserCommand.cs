using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.ApplicationUser.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteUserCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
