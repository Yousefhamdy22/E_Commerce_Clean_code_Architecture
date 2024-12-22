using E_Commerce.Application.Features.EmailsCommand.Models;
using E_Commerce.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }

   
}
