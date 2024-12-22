using E_Commerce.Application.Features.Authorization.Commands.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Authorization.Commands.Validations
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        #endregion
        #region Constructors

        #endregion
        public EditRoleValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("NotEmpty")
                 .NotNull().WithMessage("Required");
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
