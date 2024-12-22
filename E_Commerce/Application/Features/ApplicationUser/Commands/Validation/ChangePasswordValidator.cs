using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.ApplicationUser.Commands.Validation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {

        public ChangePasswordValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }


  
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID cannot be empty.")
                .NotNull().WithMessage("ID is required.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password cannot be empty.")
                .NotNull().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password cannot be empty.")
                .NotNull().WithMessage("New password is required.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("The confirmation password does not match the new password.");

        }

        public void ApplyCustomValidationsRules()
        {

        }
    }
}
