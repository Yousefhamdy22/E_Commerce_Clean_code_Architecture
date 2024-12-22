using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Command.Models
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
