using AutoMapper;
using E_Commerce.Application.Features.Categories.Query.Result;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping.Catrgories
{
    public partial class CategoriesProfile : Profile
    {
        public void GetCategoryByIdMapping()
        {

            // Map from Category to GetSingleCategoryResponse
            CreateMap<Category, GetSingleCategoryResponse>()
                .ForMember(dest => dest.productResponse, opt => opt.MapFrom(src => src.Products)); // Adjust the property names if necessary

            // Map from Product to ProductResponse
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Item_Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.solditems, opt => opt.MapFrom(src => src.solditems))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.Stock));

            // Add additional mappings if needed

        }
    }
}
