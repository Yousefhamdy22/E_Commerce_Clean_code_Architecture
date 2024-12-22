using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Products.Command.Models
{

    public class DeleteProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
