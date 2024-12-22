using E_Commerce.Application.Base;
using E_Commerce.Application.Dtos;
using MediatR;

namespace E_Commerce.Application.Features.Products.Command.Models
{
    public class AddProductCommand : IRequest<Response<string>>
    {

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public string? Description { get; set; }
        public int? solditems { get; set; }  //stock
        public int? quantity { get; set; }
        public List<IFormFile>? File { get; set; }

    }
}
