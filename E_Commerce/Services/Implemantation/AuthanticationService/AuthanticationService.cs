using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Helpers;
using E_Commerce.Infastructure.Data;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using E_Commerce.Services.Abstraction.IEmailServices;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_Commerce.Services.Implemantation.AuthanticationService
{
    public class AuthanticationService : IAuthenticationServices 
    {


        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshTokens; // map works in a concurrent environment
        private readonly UserManager<User> _userManager;
        private readonly ECommerceContext _applicationDbContext;
      private readonly IEmailServices _emailServices;
      //  private readonly IEncryptionProvider _encryptionProvider;
        #endregion

        #region Constructor
        public AuthanticationService(JwtSettings jwtSettings,
                                      UserManager<User> userManager,
                                      ECommerceContext applicationDbContext,
                                     IEmailServices emailServices)
        {
            _jwtSettings = jwtSettings;
            _userRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _emailServices = emailServices;
           // _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }
        #endregion

        #region Functions

        #region JwtTokenProcess

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GenerateJwtToken(user);
            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };

            await _applicationDbContext.userRefreshToken.AddAsync(userRefreshToken);
            await _applicationDbContext.SaveChangesAsync(); // Ensure tokens are saved

            var response = new JwtAuthResult()
            {
                AccessToken = accessToken,
                refreshToken = refreshToken,
            };

            return response;
        }

        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
        {
            // var roles = await _userManager.GetRolesAsync(user);
            var claims = await GetClaims(user);

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                TokenString = GenerateRefreshToken(),
                UserName = userName,
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            };
            return refreshToken;
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                // Include the NameIdentifier claim for UserId
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber),
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        #endregion

        #region ConfirmEmail
        public async Task<string> ConfirmEmail(int UserId, string Code)
        {
            if (UserId == null || Code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, Code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }
        #endregion

        #region ResetPasswordCode
        public async Task<string> ResetPasswordCode(string Email)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //user
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";
                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                await _emailServices.SendEmail(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        #endregion


        #region ResetPassword
        public async Task<string> ResetPassword(string Email, string Password)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                await _userManager.RemovePasswordAsync(user);
                if (!await _userManager.HasPasswordAsync(user))
                {
                    await _userManager.AddPasswordAsync(user, Password);
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        #endregion

        #region ConfirmResetPassword
        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Decrept Code From Database User Code
            var userCode = user.Code;
            //Equal With Code
            if (userCode == Code) return "Success";
            return "Failed";
        }

        #endregion




        #endregion
    }
}

