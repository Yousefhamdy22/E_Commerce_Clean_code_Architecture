using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Abstraction.IReviewServices
{
    public interface IReviewServices
    {
        public Task<string> CreateReview(Review review);
        public Task<string> DeleteReviewAsync(Review review);
        public Task<string> EditAsyncReviewAsync(Review review);
        public Task<Review> GetByIdAsync(int id);
        public Task<List<Review>> GetAllReviewsAsync();
        public Task<List<Review>> GetAllReviewsForProductAsync(int productId);
    }
}
