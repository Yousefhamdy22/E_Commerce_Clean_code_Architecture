using MediatR;
using E_Commerce.Application.Base;

namespace E_Commerce.Application.Features.EmailsCommand.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Massege { get; set; }
    }
}
