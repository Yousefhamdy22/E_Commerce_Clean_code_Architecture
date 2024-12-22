using MediatR;
using E_Commerce.Application.Base;
using E_Commerce.Domain.Helpers;

namespace E_Commerce.Application.Features.Authantication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
   

    }
}
