using AutoMapper;
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.Categories.Command.Models;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Services.Abstraction.ICategoryServices;
using MediatR;

namespace E_Commerce.Application.Features.Categories.Command.Handler
{
    public class CategoryCommandHandler : ResponseHandler,
         IRequestHandler<CreateCategoryCommand, Response<string>>,
          IRequestHandler<DeleteCategoryCommand, Response<string>>,
            IRequestHandler<UpdateCategoryCommand, Response<string>>

    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        public CategoryCommandHandler(IMapper mapper, ICategoryServices categoryServices,
            IWebHostEnvironment webHostEnvironment , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _categoryServices = categoryServices;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region HandleFunction
        public async Task<Response<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            string pictureFileName = null;

            if (request.File != null && request.File.Length > 0)
            {
                // Define the path for storing images
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/ImagesCategory");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(request.File.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Ensure the directory exists
                Directory.CreateDirectory(uploadsFolder);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                pictureFileName = uniqueFileName; // Save the file name for database storage
            }

            // Map the category and set the Picture property
            var category = _mapper.Map<Category>(request);
            category.Picture = pictureFileName;

            var result = await _categoryServices.CreateCategory(category);

            

            if (result == "Added SuccessFully")
                return Created("");
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryServices.GetByIdAsync(request.Id);

            var categoryMapper = _mapper.Map<Category>(request);
            var result = await _categoryServices.EditAsync(categoryMapper);

            if (result == "Null")
                return NotFound<string>("Category Not Exsists");

           

            return Success<string>("Success");

        }

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryServices.GetByIdAsync(request.Id);
            if (category == null)
                return NotFound<string>("category Not Exsists");
            await _categoryServices.DeleteAsync(category);
            return Deleted<string>("Delete Success");

        }

        #endregion
    }
}
