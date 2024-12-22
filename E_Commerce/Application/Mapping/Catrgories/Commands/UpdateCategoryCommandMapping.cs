using AutoMapper;
using E_Commerce.Application.Features.Categories.Command.Models;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping.Catrgories
{
    public partial class CategoriesProfile : Profile
    {
        public void UpdateCategoryCommandMapping()
        {
            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
