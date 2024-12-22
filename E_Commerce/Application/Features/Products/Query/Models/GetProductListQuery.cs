﻿using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Query.Result;
using MediatR;

namespace E_Commerce.Application.Features.Products.Query.Models
{
    public class GetProductListQuery : IRequest<Response<List<GetProductListResponse>>>
    {

    }
}
