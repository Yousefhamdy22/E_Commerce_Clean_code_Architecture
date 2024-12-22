using E_Commerce.Application.Features.Authorization.Commands.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.Application.Features.Authorization.Commands.Validations
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        public readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public DeleteRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region  Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");
        }
        // it is already exsists
        public void ApplyCustomValidationsRules()
        {
        }
        #endregion
    }
}
