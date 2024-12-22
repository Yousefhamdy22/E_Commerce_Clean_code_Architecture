using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using E_Commerce.Application.Features.ApplicationUser.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Routing;
using E_Commerce.Application.Base;
using E_Commerce.Presentation.Base;

namespace E_Commerce.Presentation.Api
{
    [Route("api/ApplicationUser")]
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            if (command == null)
            {
                
                return BadRequest("Command cannot be null.");
            }
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }


        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpGet("GetUserByID")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }
    }
}
