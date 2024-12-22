using AutoMapper;
using E_Commerce.Application.Base;
using E_Commerce.Application.Dtos;
using E_Commerce.Application.Features.Products.Command.Models;
using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Services.Abstraction.ProductService;
using MediatR;
using Shared.Dtos;

namespace E_Commerce.Application.Features.Products.Command.Handler
{
    public class ProductCommandHandler : ResponseHandler, IRequestHandler<AddProductCommand , Response<string>>
         //,IRequestHandler<UpdateProductCommand, Response<string>>,
         //IRequestHandler<DeleteProductCommand, Response<string>>

    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly IProductService _productServices;
        private readonly ICategory _categoryRepository;
      
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public ProductCommandHandler(IMapper mapper, IProductService productServices, 
            IWebHostEnvironment webHostEnvironment , ICategory categoryRepository)
        {
            _mapper = mapper;
            _productServices = productServices;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
           
        }
        #endregion
        #region HandleFunction
        public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            //Validatoin
            bool categoryExists = await _categoryRepository.CategoryExists(request.CategoryId);
            if (!categoryExists)
            {
                return new Response<string>
                {
                    Message = "Category does not exist",
                    Succeeded = false
                };
            }
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
            var product = _mapper.Map<Product>(request);

            
            product.Images = imagePaths;

            //Creation
            var result = await _productServices.CreateProduct(product);
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


        //public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await _productServices.GetByIdAsync(request.Id);
        //    var productMapper = _mapper.Map<Product>(request);
        //    var result = await _productServices.EditAsync(productMapper);

        //    if (result == "Null")
        //        return NotFound<string>("Product Not Exsists");

        //    return Success<string>("Success");

        //}

        //public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await _productServices.get(request.Id);
        //    if (product == null)
        //        return NotFound<string>("Product Not Exsists");
        //    await _productServices.DeleteAsync(product);
        //    return Deleted<string>("Delete Success");
        //}
        #endregion
    }
}
