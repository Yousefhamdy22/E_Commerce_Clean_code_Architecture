using AutoMapper;

namespace E_Commerce.Application.Mapping.Catrgories
{
    public partial class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            AddCategoryMapping();
            UpdateCategoryCommandMapping();
            GetCategoryByIdMapping();
            GetCategoryListMapping();
        }
    }
}
