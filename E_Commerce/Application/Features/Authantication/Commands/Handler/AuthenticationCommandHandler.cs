
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Authantication.Commands.Models;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Helpers;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Features.Authantication.Commands.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>
        //IRequestHandler<ResetPasswordCommand, Response<string>>,
        //IRequestHandler<SendResetPasswordCommand, Response<string>>
    {

        #region Fildes
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationServices _authenticationServices;
        #endregion

        #region Constructor
        public AuthenticationCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager
            , IAuthenticationServices authenticationServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationServices = authenticationServices;
        }

        #endregion

        #region Functions

      
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            //Return The UserName Not Found
            if (user == null)
                return BadRequest<JwtAuthResult>("UserName Is Not Exist");

            //try To Sign in 
            //    bool signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
            bool signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
            //   var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //if Failed Return Passord is wrong
            if (!signInResult)
                return BadRequest<JwtAuthResult>("Password Not Correct");
            //   if (!user.EmailConfirmed)
            //     return BadRequest<JwtAuthResult>("Email Not Confirmed");
            //Generate Token
            var result = await _authenticationServices.GetJWTToken(user);
            //return Token 
            return Success(result);
        }
        #endregion
    }
}
