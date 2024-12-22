using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Infastructure.Data;
using E_Commerce.Services.Abstraction.IApplicationUserServices;
using E_Commerce.Services.Abstraction.IEmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace E_Commerce.Services.Implemantation.ApplicationUserService
{
    public class ApplicationUserService : IApplicationUserServices
    {

        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailServices _emailsService;
        private readonly ECommerceContext _ecommerceContext;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<AddUserCommand> _logger;
        #endregion

        #region Constructors
        public ApplicationUserService(UserManager<User> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailServices emailsService,
                                      ECommerceContext ecommerceContext,
                                      IUrlHelperFactory urlHelperFactory,
                                      IActionContextAccessor actionContextAccessor ,
                                      ILogger<AddUserCommand> logger)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _ecommerceContext = ecommerceContext;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            _logger = logger;
        }
        #endregion


        #region Handle Functions

        #region AddUser

        public async Task<string> AddUserAsync(User user, string password)
        {
            using var transaction = await _ecommerceContext.Database.BeginTransactionAsync();
            try
            {
                // Trim and validate email
                user.Email = user.Email.Trim();
                if (!IsValidEmail(user.Email))
                {
                    _logger.LogError($"Invalid email format: {user.Email}");
                    return "EmailInvalid"; // Update to match Handle method.
                }

                // Check if email already exists
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning($"Email already exists: {user.Email}");
                    return "EmailIsExist";
                }

                // Check if username already exists
                var existingUserName = await _userManager.FindByNameAsync(user.UserName);
                if (existingUserName != null)
                {
                    _logger.LogWarning($"Username already exists: {user.UserName}");
                    return "UserNameIsExist";
                }

                // Create the user
                var createResult = await _userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                {
                    _logger.LogError("User creation failed: {Errors}", 
                        string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    return "ErrorInCreateUser"; // Update to match Handle method.
                }
               
                // Assign user to default role
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    return "ErrorInCreateUser";
                }

                // Generate email confirmation token
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Construct email confirmation link
                var request = _httpContextAccessor.HttpContext.Request;
                var confirmationLink = $"{request.Scheme}://{request.Host}{_urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = emailConfirmationToken })}";

                // Send confirmation email
                var emailMessage = $"To confirm your email, please click the following link: <a href='{confirmationLink}'>Confirm Email</a>";
                await _emailsService.SendEmail(user.Email, "Email Confirmation", emailMessage);

                // Commit transaction
                await transaction.CommitAsync();
                _logger.LogInformation("User created successfully: {UserId}", user.Id);
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration");
                await transaction.RollbackAsync();
                return "ErrorOccurred";
            }
        }

        //public async Task<string> AddUserAsync(User user, string password)
        //{
        //    var trans = await _ecommerceContext.Database.BeginTransactionAsync();
        //    try
        //    {

        //        user.Email = user.Email.Trim();

        //        if (!IsValidEmail(user.Email))
        //        {
        //            _logger.LogError($"Invalid email format: {user.Email}");
        //            return "InvalidEmailFormat";
        //        }


        //        //if Email is Exist
        //        var existUser = await _userManager.FindByEmailAsync(user.Email);
        //        //email is Exist
        //        if (existUser != null) return "EmailIsExist";

        //        //if username is Exist
        //        var userByUserName = await _userManager.FindByNameAsync(user.UserName);
        //        //username is Exist
        //        if (userByUserName != null) return "UserNameIsExist";
        //        //Create
        //        var createResult = await _userManager.CreateAsync(user, password);

        //        //Failed
        //        if (!createResult.Succeeded)
        //            return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

        //        await _userManager.AddToRoleAsync(user, "User");

        //        // Send Confirm Email
        //        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //        var resquestAccessor = _httpContextAccessor.HttpContext.Request;
        //        var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail",
        //            "Authentication", new { userId = user.Id, code = code });

        //        var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";


        //        //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
        //        //message or body
        //        //   await _emailsService.SendEmail(user.Email, message, "ConFirm Email");

        //        await trans.CommitAsync();
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        await trans.RollbackAsync();

        //        _logger.LogError($"An error occurred during user creation: {ex.Message}");
                
        //        return "Failed";
        //    }

        //}
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        #endregion





        #endregion


    }
}
