using E_Commerce.Application.Features.Authantication.Commands.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Authantication.Commands.Validations
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Constuctors
        public SignInValidator()
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name cannot be empty.")
                .NotNull().WithMessage("User name is required.");
              
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password is required.");

        }
        public void ApplyCustomValidationRules()
        {


        }
        #endregion

    }
}
