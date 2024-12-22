using MediatR;
using E_Commerce.Application.Base;

namespace E_Commerce.Application.Features.Authantication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
