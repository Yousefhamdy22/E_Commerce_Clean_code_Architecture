using E_Commerce.Application.Features.Authantication.Query.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Authantication.Query.Validations
{
    public class ConfirmResetPasswordValidations : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Fields
        #endregion

        #region Constructors
        public ConfirmResetPasswordValidations()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Code)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");

        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion

    }
}
