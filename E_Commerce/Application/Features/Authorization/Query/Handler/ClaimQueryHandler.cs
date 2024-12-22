using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authorization.Query.Models;
using E_Commerce.Domain.Dtos;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Features.Authorization.Query.Handler
{
    public class ClaimQueryHandler : ResponseHandler,
          IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResults>>
    {
        #region Fileds
        private readonly IAuthorizationServices _authorizationService;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructors
        public ClaimQueryHandler(IAuthorizationServices authorizationService,
                                  UserManager<User> userManager)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<ManageUserClaimsResults>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<ManageUserClaimsResults>("User Is Not Found");
            var result = await _authorizationService.ManageUserClaimData(user);
            return Success(result);
        }
        #endregion
    }
}
