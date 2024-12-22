using E_Commerce.Domain.Entities;
using E_Commerce.Services.Abstraction.IReviewServices;

namespace E_Commerce.Services.Implemantation.ReviewService
{
    public class ReviewService : IReviewServices
    {
        public Task<string> CreateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditAsyncReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviewsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviewsForProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
