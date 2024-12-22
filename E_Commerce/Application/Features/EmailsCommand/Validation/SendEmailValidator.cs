using E_Commerce.Application.Features.EmailsCommand.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.EmailsCommand.Validation
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        #endregion
        #region Constructors
        public SendEmailValidator()
        {

            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Not Empty")
                 .NotNull().WithMessage("Required");
            RuleFor(x => x.Massege)
                 .NotEmpty().WithMessage("Not Empty")
                 .NotNull().WithMessage("Required");
        }
        #endregion
    }
}
