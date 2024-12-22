using DashBoard.Application.DTOs;
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Query.Result;
using MediatR;

namespace DashBoard.Application.Features.Products.Query.Models
{
    public class DashGetProductListQuery : IRequest<Response<List<DashboardProductDto>>>
    {

    }
}
