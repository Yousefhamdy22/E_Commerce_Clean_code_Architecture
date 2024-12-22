using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authantication.Query.Models;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Features.Authantication.Query.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,
           IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ConfirmResetPasswordQuery, Response<string>>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationServices _authenticationServices;

        #endregion
        #region Constructor
        public AuthenticationQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager
            , IAuthenticationServices authenticationServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationServices = authenticationServices;
        }




        #endregion
        #region HandleFunctions



        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationServices.ConfirmEmail(request.UserId, request.Code);
            if (confirmEmail == "ErrorWhenConfirmEmail")
                return BadRequest<string>("Error When ConfirmEmail");
            return Success<string>("ConfirmEmail Done");
        }


        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationServices.ConfirmResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>("UserIsNotFound");
                case "Failed": return BadRequest<string>("InvaildCode");
                case "Success": return Success<string>("");
                default: return BadRequest<string>("InvaildCode");
            }
        }
        #endregion
    }
}
