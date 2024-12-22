using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Command.Models
{
    public class UpdateCategoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public int? TotalProducts { get; set; }
    }
}
