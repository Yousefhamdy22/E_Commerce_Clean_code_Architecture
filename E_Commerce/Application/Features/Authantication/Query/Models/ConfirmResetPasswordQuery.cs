using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Authantication.Query.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
