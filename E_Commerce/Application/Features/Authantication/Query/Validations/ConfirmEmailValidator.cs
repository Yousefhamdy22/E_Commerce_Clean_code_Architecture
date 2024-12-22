using E_Commerce.Application.Features.Authantication.Query.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Authantication.Query.Validations
{
    public class ConfirmEmailValidator
     : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        #endregion

        #region Constructors
        public ConfirmEmailValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("NotEmpty")
                .NotNull().WithMessage("Required");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion

    }
}
