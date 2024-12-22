using E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Services.Abstraction.IApplicationUserServices
{
    public interface IApplicationUserServices
    {

        public Task<string> AddUserAsync(User user, string password);
    }
}
