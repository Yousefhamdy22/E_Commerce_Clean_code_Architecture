using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Helpers;

namespace E_Commerce.Services.Abstraction.IAuthenticationServices
{
    public interface IAuthenticationServices
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<string> ConfirmEmail(int UserId, string Code);
        public Task<string> ResetPasswordCode(string Email);
        public Task<string> ResetPassword(string Email, string Password);
        public Task<string> ConfirmResetPassword(string Code, string Email);

    }
}
