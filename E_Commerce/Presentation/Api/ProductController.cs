
using E_Commerce.Application.Features.Products.Command.Models;
using E_Commerce.Application.Features.Products.Query.Models;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Presentation.Base;
using E_Commerce.Services.Abstraction.ProductService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Presentation.Api
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : AppControllerBase
    {

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] AddProductCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut("EditProduct")]
        public async Task<IActionResult> Edit([FromForm] UpdateProductCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));
            return NewResult(response);
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await Mediator.Send(new GetProductListQuery());
            return NewResult(response);
        }
        [HttpGet("GetByID")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var response = await Mediator.Send(new GetProductByIDQuery(id));
            return NewResult(response);
        }
        [HttpGet("GetByname")]
        public async Task<IActionResult> GetByname(string name)
        {
            var response = await Mediator.Send(new GetProductByNameQuery(name));
            return NewResult(response);
        }
        [HttpGet("ProductsPaginated")]
        public async Task<IActionResult> Paginated([FromQuery] GetProductPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("GetAllProductBySortReview")]
        public async Task<IActionResult> GetAllProductBySortReview()
        {
            var response = await Mediator.Send(new GetAllProductSortByReviewQuery());
            return NewResult(response);
        }
    }
}