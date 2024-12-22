using AutoMapper;
using DashBoard.Application.DTOs;
using DashBoard.Application.Features.Products.Query.Models;
using DashBoard.Services.Abstractions;
using E_Commerce.Application.Base;
using E_Commerce.Services.Abstraction.IImageServices;
using MediatR;



namespace DashBoard.Application.Features.Products.Query.Handler
{
    public class DashProductsQueryHandler : ResponseHandler ,
              IRequestHandler<DashGetProductListQuery, Response<List<DashboardProductDto>>>
    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly IDashboardProductService _dashproductServices;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        #endregion

        #region Constructor

        public DashProductsQueryHandler(IMapper mapper, IDashboardProductService dashproductServices,
          IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _dashproductServices = dashproductServices;
           
            _httpContextAccessor = httpContextAccessor;
          
        }
        #endregion

        #region GetProductListQuery
        public async Task<Response<List<DashboardProductDto>>> Handle(DashGetProductListQuery request,
                                                                        CancellationToken cancellationToken)
        {
            var products = await _dashproductServices.GetAllProductsAsync();

            if (products == null || !products.Any())
                return NotFound<List<DashboardProductDto>>("Products not found");

            var mappedResponse =  _mapper.Map<List<DashboardProductDto>>(products);


            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return BadRequest<List<DashboardProductDto>>("HttpContext is not available");

            // Handle ImageUrls (Optional or Default Values)
            foreach (var product in mappedResponse)
            {
                product.ImageUrls = new List<string>
        {
            $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/default.png"
        };
            }

            return Success<List<DashboardProductDto>>(mappedResponse);
        }

        #endregion

    }
}
