﻿using AutoMapper;
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authorization.Query.Models;
using E_Commerce.Application.Features.Authorization.Query.Result;
using E_Commerce.Domain.Dtos;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Features.Authorization.Query.Handler
{
    public class RoleQueryHandler : ResponseHandler,
      IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
           IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>
    {

        #region Properties
        private readonly IAuthorizationServices _authorizationServices;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor
        public RoleQueryHandler(IMapper mapper, IAuthorizationServices authorizationServices,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _authorizationServices = authorizationServices;
            _userManager = userManager;
        }
        #endregion

        #region Functions

        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationServices.GetRolesAsync();
            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);
        }
        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationServices.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResult>("NotFound");
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }
        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRolesResult>("User Is Not Found");
            var result = await _authorizationServices.ManageUserRolesData(user);
            return Success(result);
        }
        #endregion
    }
}
