using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.ApplicationUser.Commands.Models
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
