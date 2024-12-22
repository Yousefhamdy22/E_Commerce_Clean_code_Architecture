using E_Commerce.Application.Features.Authorization.Commands.Models;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
using FluentValidation;

namespace E_Commerce.Application.Features.Authorization.Commands.Validations
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IAuthorizationServices _authorizationService;
        #endregion
        #region Constructors

        #endregion
        public AddRoleValidator(IAuthorizationServices authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExstists(Key))
                .WithMessage("IsExist");
        }

        #endregion
    }
}
