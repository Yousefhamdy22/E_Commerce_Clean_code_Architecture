using DashBoard.Application.Features.Products.Commands.Models;
using DashBoard.Application.Features.Products.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.Presentation.Controllers
{
    [Route("DashProducts")]
    public class DashProductsController : Controller
    {
        private readonly IMediator _mediator;

        public DashProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View("Create");
        }


        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> SaveAddProduct([FromForm] DashAddProductCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            return View("Create");
        }



        [Route("index")]
        [HttpGet]
        public async Task<IActionResult> index()
        {
            var response = await _mediator.Send(new DashGetProductListQuery());
            return View(response);
        }
    }
}
