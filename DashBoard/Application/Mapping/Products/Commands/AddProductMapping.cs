using AutoMapper;
using DashBoard.Application.DTOs;
using DashBoard.Application.Features.Products.Commands.Models;
using DashBoard.Core.Entities;
using Shared.Dtos;

namespace DashBoard.Application.Mapping.Products
{
    public partial class DashProductsProfile : Profile
    {
        public void AddProductMapping()
        {
            CreateMap<DashAddProductCommand, DashProduct>();
            CreateMap<ProductDto, DashboardProductDto>()
                .ForMember(dest => dest.Stock, opt => opt.Ignore())
                .ForMember(dest => dest.SolidItems, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.DisplayCategory ,  opt => opt.Ignore()) 
                .ForMember(dest => dest.IsAvalible ,  opt => opt.Ignore()) 
            .ReverseMap();

            CreateMap<DashboardProductDto, DashProduct>().ReverseMap();

            // Map between DashProduct and DashProductDto
            CreateMap<DashProduct, DashboardProductDto>().ReverseMap();

            // Map between DashProductDto and ProductDto (if needed)
         

            // Map between DashProduct and ProductDto (if directly needed)
           

        }
    }
}
