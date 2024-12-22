﻿using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authorization.Commands.Models;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Commands.Handler
{
    public class AuthorizationCommandHandler : ResponseHandler,
         IRequestHandler<AddRoleCommand, Response<string>>,
         //IRequestHandler<EditRoleCommand, Response<string>>,
         IRequestHandler<DeleteRoleCommand, Response<string>>,
             IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Feilds
        private readonly IAuthorizationServices _authorizationServices;
        #endregion

        #region Constructor
        public AuthorizationCommandHandler(IAuthorizationServices authorizationServices)
        {
            _authorizationServices = authorizationServices;
        }
        #endregion

        #region HandleFunctions

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.AddRoleAsync(request.RoleName);
            if (result == "Success")
            {
                return Success("");
            }
            return BadRequest<string>("FaliedToAddRole");
        }

        //public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        //{
        //    var result = await _authorizationServices.EditRoleAsync(request);
        //    if (result == "notFound") return NotFound<string>();
        //    else if (result == "Success") return Success((string)"Success");
        //    else
        //        return BadRequest<string>(result);
        //}

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>("RoleIsUsed");//NOOO
            else if (result == "Success") return Success((string)"Deleted");
            else
                return BadRequest<string>(result);
        }
        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>("User Is Not Found");
                case "FailedToRemoveOldRoles": return BadRequest<string>("Failed To Remove Old Roles");
                case "FailedToAddNewRoles": return BadRequest<string>("Failed To Add New Roles");
                case "FailedToUpdateUserRoles": return BadRequest<string>("Failed To Update User Roles");
            }
            return Success<string>("Success");
        }
        #endregion
    }
}
