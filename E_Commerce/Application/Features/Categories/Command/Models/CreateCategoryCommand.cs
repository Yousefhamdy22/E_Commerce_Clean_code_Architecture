using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Command.Models
{
    public class CreateCategoryCommand : IRequest<Response<string>>
    {
        public IFormFile? File { get; set; }  // Add this line for file upload
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //     public string? Picture { get; set; }
        public int? TotalProducts { get; set; }
        //    IFormFile file { get; set; }

    }
}
