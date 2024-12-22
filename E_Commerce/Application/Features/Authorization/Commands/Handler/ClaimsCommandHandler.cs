using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authorization.Commands.Models;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler : ResponseHandler,
       IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fileds
        private readonly IAuthorizationServices _authorizationService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler(IAuthorizationServices authorizationService)
        {
            _authorizationService = authorizationService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>("User Is Not Found");
                case "FailedToRemoveOldClaims": return BadRequest<string>("Failed To Remove Old Claims");
                case "FailedToAddNewClaims": return BadRequest<string>("Failed To Add New Claims");
                case "FailedToUpdateClaims": return BadRequest<string>("Failed To Update Claims");
            }
            return Success<string>("Success");
        }
        #endregion
    }
}
