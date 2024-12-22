
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Helpers;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Services.Abstraction.ProductService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services.Implemantation.ProductService
{
    public class ProductService : IProductService
    {


        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _productRepo;
        private readonly ILogger<ProductService> _logger;

        #endregion


        #region Constractor
        public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger, IProduct productRepo)
        {
            _unitOfWork = unitOfWork;
            _productRepo = productRepo;
            _logger = logger;
        }
        #endregion


        #region Search
        public IEnumerable<Product> SearchProducts(string searchTerm)
        {

            try
            {
                #region Ex
                //var products = _unitOfWork.ProductsRepo // StringComparison.OrdinalIgnoreCase problem
                //          .GetAll()
                //          .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                //                      p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                //          .ToList();  // 
                //--------------

                //var products =  _unitOfWork.ProductsRepo  // right
                //   .GetAll()
                //   .Where(p => p.Name.Contains(searchTerm.Trim() )
                //    || p.Description.Contains(searchTerm.Trim())).ToList();

                #endregion

                var products = _unitOfWork.ProductsRepo
                   .GetAll()
                   .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%")) // Using LIKE for searching
                   .ToList();




                _logger.LogInformation("{Count} products found for search term {SearchTerm}", products.Count, searchTerm);

                return products;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products with term {SearchTerm}", searchTerm);

                throw;
            }

        }
        #endregion

        #region GetProductsByCategoryAsync
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                var categoryItems = await _unitOfWork.ProductsRepo
                    .GetAll().Where(c => c.Categoryid == categoryId)
                    .ToListAsync();

                _logger.LogInformation("{Count} products found for category {CategoryId}", categoryItems.Count, categoryId);
                return categoryItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products for category {CategoryId}", categoryId);
                throw;
            }
        }
        #endregion


        #region Functions

        
        public async Task<string> CreateProduct(Product product)
        {
           

            await _productRepo.Add(product);
            await _unitOfWork.Save();
            return "Added SuccessFully";

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepo.GetTableNoTracking().Include(x => x.Images)
               .Where(x => x.ProductId.Equals(id)).FirstOrDefaultAsync();

           
            return product;
        }

        public async Task<List<Product>> GetByName(string name)
        {
            var product = await _productRepo.GetTableNoTracking().Include
                 (x => x.Images).Where(p => p.Name.Equals(name)).ToListAsync();
            
            return product;
        }

        #region PaginatedQuerable
        public IQueryable<Product> FilterProductPaginatedQuerable(ProductOrderingEnum orderingEnum, string Search)
        {
            var querable = _productRepo.GetTableNoTracking().Include(x => x.Images).AsQueryable();

            if (Search != null)
                querable = querable.Where(x => x.Name.Contains(Search));

            switch(orderingEnum)
            {
                case ProductOrderingEnum.Id:
                    querable = querable.OrderBy(x => x.ProductId);
                    break;
                case ProductOrderingEnum.Item_Name:
                    querable = querable.OrderBy(x => x.Name);
                    break;
                default:
                    querable = querable.OrderBy(x => x.Price);
                    break;
            }
           
            return querable;

        }

        #endregion

        public async Task<List<Product>> GetProductListAsync()
        {
            var products = await _productRepo.GetTableNoTracking().Include(x => x.Images).ToListAsync();
            if (!products.Any())
                return null;
            
            return products;
        }
        public Task<List<Product>> GetAllBySortReviewAsync()
        {
            throw new NotImplementedException();
        }
        public IQueryable<Product> GetAllProductsQueryable()
        {
            return _productRepo.GetTableNoTracking().Include(x => x.Images).AsQueryable();
        }

        #endregion

    }
}
