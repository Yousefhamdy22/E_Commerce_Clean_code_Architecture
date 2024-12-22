using AutoMapper;
using E_Commerce.Application.Features.ApplicationUser.Query.Models;
using E_Commerce.Application.Mapping.ApplicarionUser.Command;

namespace E_Commerce.Application.Mapping.ApplicarionUser.Command;

public partial class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        AddUserMapping();
        UpdateUserMapping();
        //GetUserByIdMapping();

    }

   
}
