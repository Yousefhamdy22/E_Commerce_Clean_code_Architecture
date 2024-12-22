using E_Commerce.Application.Dtos;
using E_Commerce.Application.Features.ApplicationUser.Commands.Handlers;
using E_Commerce.Application.Features.Cart.Command.Handler;
using E_Commerce.Application.Features.Cart.Command.Models;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Presentation.Base;
using E_Commerce.Services.Abstraction.ShoppingCartService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace E_Commerce.Presentation.Api
{
    [Route("api/ShoppingCart")]
    [ApiController]
    public class ShoppingCartController : AppControllerBase
    {
       

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddCartCommand addCartCommand)
        {

            try
            {
               
                var cartDto = await Mediator.Send(addCartCommand);

                return Ok(cartDto);
            }
            catch (Exception )
            {
               
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        //[HttpGet("get-cart")]
        //public async Task<IActionResult> GetCart()
        //{
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //    // Log the value of userIdClaim for debugging purposes

        //    if (!int.TryParse(userIdClaim, out int userId))
        //    {
        //        return BadRequest("Invalid user ID.");
        //    }

        //    // Pass the userId to the constructor
        //    var query = new GetCartsByUserIdQuery(userId);
        //    var response = await Mediator.Send(query);
        //    return NewResult(response);
        //}

        //[HttpGet("total-payment")]
        //public async Task<IActionResult> GetTotalPayment()
        //{
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //    if (!int.TryParse(userIdClaim, out int userId))
        //    {
        //        return BadRequest("Invalid user ID.");
        //    }
        //    var query = new GetTotalPaymentQuery(userId);
        //    var response = await Mediator.Send(query);
        //    return NewResult(response);
        //}
        [HttpDelete("remove-from-cart")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveCartCommand command)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID.");
            }
            command.UserId = userId;
            var response = await Mediator.Send(command);
            return NewResult(response);
        }



    }
}
