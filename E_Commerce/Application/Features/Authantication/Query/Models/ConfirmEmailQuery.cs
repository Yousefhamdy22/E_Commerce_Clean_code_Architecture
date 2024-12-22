using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Authantication.Query.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
