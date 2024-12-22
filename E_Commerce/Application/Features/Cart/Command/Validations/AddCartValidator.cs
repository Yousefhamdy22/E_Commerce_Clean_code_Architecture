using E_Commerce.Application.Features.Cart.Command.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Cart.Command.Validations
{
    public class AddCartValidator : AbstractValidator<AddCartCommand>
    {
        #region Fields
        #endregion
        #region Constructors
        public AddCartValidator()
        {
            ApplyValidationsRules();
            //  ApplyCustomValidationsRules();
        }

        #endregion
        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.CartItemDto.ProductId)
                       .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

            //RuleFor(x => x.)
            //    .GreaterThanOrEqualTo(0).WithMessage("Quantity must be 0 or greater.");

            //RuleFor(x => x.Date)
            //    .NotEmpty().WithMessage("Date cannot be empty.");
            //RuleFor(x => x.UserId)
            //    .GreaterThan(0).WithMessage("UserId must be greater than 0.");
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
