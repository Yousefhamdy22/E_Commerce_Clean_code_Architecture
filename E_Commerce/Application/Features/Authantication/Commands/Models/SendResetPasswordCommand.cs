using MediatR;
using E_Commerce.Application.Base;

namespace E_Commerce.Application.Features.Authantication.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
    
    
}
