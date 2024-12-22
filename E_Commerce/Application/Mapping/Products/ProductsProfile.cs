using AutoMapper;
using E_Commerce.Application.Features.Products.Command.Models;
using System.Collections.Generic;

namespace E_Commerce.Application.Mapping.Products
{
    public partial class ProductsProfile : Profile
    {
       public ProductsProfile()
        {
            GetProductListMapping();
            AddProductMapping();
            GetProductByIdMapping();
            UpdateProduct();
        }
    }
}
