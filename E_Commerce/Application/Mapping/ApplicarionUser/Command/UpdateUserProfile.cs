using AutoMapper;
using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Application.Mapping.ApplicarionUser.Command
{
    public partial class ApplicationUserProfile : Profile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
