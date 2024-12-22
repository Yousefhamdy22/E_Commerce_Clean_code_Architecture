using AutoMapper;
using DashBoard.Application.DTOs;
using DashBoard.Application.Features.Products.Commands.Models;
using DashBoard.Core.Entities;
using DashBoard.Infrastructure.Services.Abstractions;
using DashBoard.Services.Abstractions;
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Products.Command.Handler;
using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Services.Abstraction.ProductService;
using MediatR;
using Shared.Dtos;

namespace DashBoard.Application.Features.Products.Commands.Handler
{
    public class DashProductCommandHandler : ResponseHandler, IRequestHandler<DashAddProductCommand, Response<string>>
    {



        #region Feilds
        private readonly IMapper _mapper;
        private readonly IproductDashRepo _productServices;
        private readonly IDashboardProductService _dashboardProductService;

        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public DashProductCommandHandler(IMapper mapper, IproductDashRepo productServices,
            IWebHostEnvironment webHostEnvironment, IDashboardProductService dashboardProductService)
        {
            _mapper = mapper;
            _productServices = productServices;
            _dashboardProductService = dashboardProductService;
            _webHostEnvironment = webHostEnvironment;

        }
        #endregion


        #region HandleFunction
        public async Task<Response<string>> Handle(DashAddProductCommand request, CancellationToken cancellationToken)
        {
            //Validatoin
            //bool categoryExists = await _categoryRepository.CategoryExists(request.CategoryId);
            //if (!categoryExists)
            //{
            //    return new Response<string>
            //    {
            //        Message = "Category does not exist",
            //        Succeeded = false
            //    };
            //}
            //Handle Upload
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder);

            // Initialize a list to hold the image paths
            var imagePaths = new List<ProductImages>();

            foreach (var file in request.File)
            {
                if (file != null && file.Length > 0)
                {

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                    Directory.CreateDirectory(uploadsFolder);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    // Create a ProductImages instance and add it to the list
                    imagePaths.Add(new ProductImages
                    {

                        ImagePath = uniqueFileName,

                    });
                }
            }

            // map the request DTO to the domain model
            var product = _mapper.Map<DashboardProductDto>(request);


            //product.Images = imagePaths ;

            //Creation
            var result = await _dashboardProductService.CreateProduct(product);
            // map Data For Response 
            var productDto = _mapper.Map<ProductDto>(result);

            // Returing Response
            return new Response<string>
            {
                //  Data = productDto,
                Message = "Product added successfully",
                Succeeded = true
            };

        }

        #endregion
    }
}
