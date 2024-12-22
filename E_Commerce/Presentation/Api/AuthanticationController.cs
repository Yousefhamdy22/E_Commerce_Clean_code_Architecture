using E_Commerce.Application.Features.Authantication.Commands.Handler;
using E_Commerce.Application.Features.Authantication.Commands.Models;
using E_Commerce.Application.Features.Authantication.Query.Models;
using E_Commerce.Presentation.Base;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;





namespace E_Commerce.Presentation.Api
{
    [Route("api/Authantication")]
    [ApiController]
    public class AuthanticationController : AppControllerBase
    {

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [HttpPost("SendResetPasswordCode")]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("ConfirmResetPasswordCode")]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
