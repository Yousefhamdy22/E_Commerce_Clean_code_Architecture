using E_Commerce.Application.Features.Categories.Query.Models;
using E_Commerce.Application.Features.Categories.Query.Result;
using E_Commerce.Application.Features.Categories.Command.Models;
using E_Commerce.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.Api
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : AppControllerBase
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;
        //public CategoryController(IWebHostEnvironment webHostEnvironment )
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Create([FromForm] CreateCategoryCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UpdateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteCategoryCommand(id));
            return NewResult(response);
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetCategoryByID(int id)
        {
            var response = await Mediator.Send(new GetCategoryByIDQuery(id));
            return NewResult(response);
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var response = await Mediator.Send(new GetCategoryListQuery());
            return NewResult(response);
        }
        [HttpGet("GetByname")]
        public async Task<IActionResult> GetByname(string name)
        {
            var response = await Mediator.Send(new GetCategoryByNameQuery(name));
            return NewResult(response);
        }
    }
}
