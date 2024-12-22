using AutoMapper;
using E_Commerce.Application.Features.ApplicationUser.Query.Result;
using E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Application.Mapping.ApplicarionUser.Query
{
    public partial class ApplicationUserProfile : Profile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
