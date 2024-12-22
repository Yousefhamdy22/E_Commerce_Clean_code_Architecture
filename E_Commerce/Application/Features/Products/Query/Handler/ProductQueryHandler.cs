using AutoMapper;
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Query.Models;
using E_Commerce.Application.Features.Products.Query.Result;
using E_Commerce.Application.Wrapper;
using E_Commerce.Domain.Entities;
using E_Commerce.Services.Abstraction.IImageServices;
using E_Commerce.Services.Abstraction.IReviewServices;
using E_Commerce.Services.Abstraction.ProductService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce.Application.Features.Products.Query.Handler
{
    public class ProductQueryHandler : ResponseHandler,
         IRequestHandler<GetProductListQuery, Response<List<GetProductListResponse>>>,
           IRequestHandler<GetProductByIDQuery, Response<GetSingleProductResponse>>,
             IRequestHandler<GetProductByNameQuery, Response<List<GetProductListResponse>>>,
             IRequestHandler<GetProductPaginatedListQuery, PaginatedResult<GetProductPaginatedListResponse>>,
                     IRequestHandler<GetAllProductSortByReviewQuery, Response<List<GetAllProductSortByReviewResponse>>>


    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly IProductService _productServices;
        private readonly IImageServices _imageServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReviewServices _reviewServices;
        #endregion

        #region Constructor

        public ProductQueryHandler(IMapper mapper, IProductService productServices,
           IImageServices imageServices, IHttpContextAccessor httpContextAccessor, IReviewServices reviewServices)
        {
            _mapper = mapper;
            _productServices = productServices;
           _imageServices = imageServices;
            _httpContextAccessor = httpContextAccessor;
            _reviewServices = reviewServices;
        }
        #endregion

        #region HandleFunction

        #region GetProductListQuery
        public async Task<Response<List<GetProductListResponse>>> Handle(GetProductListQuery request,
                                                                        CancellationToken cancellationToken)
        {
            var products = await _productServices.GetProductListAsync();

            if (products == null || !products.Any())
                return NotFound<List<GetProductListResponse>>("Products not found");

            var mappedResponse = _mapper.Map<List<GetProductListResponse>>(products);

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return BadRequest<List<GetProductListResponse>>("HttpContext is not available");

            foreach (var product in mappedResponse)
            {
                var images = await _imageServices.GetProductImagesByProductIdAsync(product.id);
                product.ImageUrls = images.Select(img => $"{httpContext.Request.Scheme}" +
                $"://{httpContext.Request.Host}/images/{img.ImagePath}").ToList();
            }

            return Success<List<GetProductListResponse>>(mappedResponse);
        }
        #endregion


        #region GetProductByIDQuery

        public async Task<Response<GetSingleProductResponse>> Handle(GetProductByIDQuery request,
            CancellationToken cancellationToken)
        {
            var response = await _productServices.GetByIdAsync(request.Id);

            if (response == null)
                return NotFound<GetSingleProductResponse>("Product not found");

            var mappedResponse = _mapper.Map<GetSingleProductResponse>(response);
            //imaged
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return BadRequest<GetSingleProductResponse>("HttpContext is not available");
            var images = await _imageServices.GetProductImagesByProductIdAsync(mappedResponse.id);
            mappedResponse.ImageUrls = images.Select(img => $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/{img.ImagePath}").ToList();
            return Success<GetSingleProductResponse>(mappedResponse);
        }
        #endregion


        #region GetProductByNameQuery
        public async Task<Response<List<GetProductListResponse>>> Handle(GetProductByNameQuery request,
                                                                         CancellationToken cancellationToken)
        {
            var products = await _productServices.GetByName(request.Name);

            if (products == null || !products.Any())
                return NotFound<List<GetProductListResponse>>("Products not found");

            var mappedResponse = _mapper.Map<List<GetProductListResponse>>(products);

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return BadRequest<List<GetProductListResponse>>("HttpContext is not available");

            foreach (var product in mappedResponse)
            {
                var images = await _imageServices.GetProductImagesByProductIdAsync(product.id);
                if (images != null && images.Any())
                {
                    // Build absolute URLs for images
                    product.ImageUrls = images.Select(img => $"{httpContext.Request.Scheme}://" +
                                                            $"{httpContext.Request.Host}/images/{img.ImagePath}")
                                               .ToList();
                }
                else
                {
                    // If no images are found, initialize an empty list
                    product.ImageUrls = new List<string>();
                }
              
            }

            return Success<List<GetProductListResponse>>(mappedResponse);
        }
        #endregion


        #region GetProductPaginatedListQuery
        public async Task<PaginatedResult<GetProductPaginatedListResponse>> Handle(GetProductPaginatedListQuery request, CancellationToken cancellationToken)
        {
            // Get the HTTP context
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is not available");

            // Define the base URL for images
            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/";

            // Define the projection expression
            Expression<Func<Product, GetProductPaginatedListResponse>> projection = product =>
                new GetProductPaginatedListResponse(
                    product.ProductId,
                    product.Categoryid,
                    product.Name,
                    product.Price,
                    product.Description,
                    product.Stock,
                    product.Stock,
                    product.Images != null ? product.Images.Select(img => $"{baseUrl}{img.ImagePath}").ToList() : new List<string>()
                );

            // Get the queryable products and apply filtering
            IQueryable<Product> queryableProducts = _productServices.GetAllProductsQueryable();
            IQueryable<Product> filteredProducts = _productServices.FilterProductPaginatedQuerable(request.OrderBy, request.Search);

            // Apply pagination and projection
            var paginatedProducts = await filteredProducts
                .Select(projection)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            // Add meta information
            paginatedProducts.Meta = new
            {
                Count = await filteredProducts.CountAsync(cancellationToken)
            };

            return paginatedProducts;
        }
        #endregion


        #region GetAllProductSortByReviewQuery
        public async Task<Response<List<GetAllProductSortByReviewResponse>>> Handle(GetAllProductSortByReviewQuery request, CancellationToken cancellationToken)
        {
            var products = await _productServices.GetAllBySortReviewAsync();

            if (products == null || !products.Any())
                return NotFound<List<GetAllProductSortByReviewResponse>>("Products not found");

            var mappedResponse = _mapper.Map<List<GetAllProductSortByReviewResponse>>(products);

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return BadRequest<List<GetAllProductSortByReviewResponse>>("HttpContext is not available");

            foreach (var product in mappedResponse)
            {
                var images = await _imageServices.GetProductImagesByProductIdAsync(product.id);
                product.ImageUrls = images.Select(img => $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/{img.ImagePath}").ToList();
                //  var reviews = await _reviewServices.GetAllReviewsForProductAsync(product.id);
                // int averageRate = (int)reviews.Average(r => r.Rate);
                //   product.averageRate = averageRate;
            }

            return Success<List<GetAllProductSortByReviewResponse>>(mappedResponse);
        }
        #endregion











        #endregion
    }
}
