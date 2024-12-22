using AutoMapper;
using E_Commerce.Application.Features.Products.Query.Result;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping.Products
{
    public partial class ProductsProfile : Profile
    {
        public void GetProductListMapping()
        {
            CreateMap<Product, GetProductListResponse>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Categoryid))
                .ForMember(dest => dest.Item_Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.solditems, opt => opt.MapFrom(src => src.solditems))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images));

        }
    }
}
