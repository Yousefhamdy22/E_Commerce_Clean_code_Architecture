using E_Commerce.Application.Base;
using E_Commerce.Application.Dtos;
using MediatR;

namespace E_Commerce.Application.Features.Products.Command.Models
{
    public class UpdateProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Item_Name { get; set; }
        public int price { get; set; }
        public string? Description { get; set; }
        public int? solditems { get; set; }
        public int? quantity { get; set; }
    }
}
