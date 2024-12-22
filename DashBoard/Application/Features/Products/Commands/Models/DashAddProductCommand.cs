using DashBoard.Application.DTOs;
using DashBoard.Core.Entities;
using E_Commerce.Application.Base;
using MediatR;

namespace DashBoard.Application.Features.Products.Commands.Models
{
    public class DashAddProductCommand : DashboardProductDto , IRequest<Response<string>>
    {

        public List<IFormFile>? File { get; set; }

    }
}
