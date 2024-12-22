using E_Commerce.Application.Features.Products.Command.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Products.Command.Validation
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator()
        {
            ApplyValidationRules();
            //   ApplyCustomValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Item Name is required.")
                .MaximumLength(150).WithMessage("Item Name cannot exceed 150 characters.");

            RuleFor(x => x.price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be 0 or greater.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.solditems)
                .GreaterThanOrEqualTo(0).When(x => x.solditems.HasValue).WithMessage("Sold Items must be 0 or greater.");

            RuleFor(x => x.quantity)
                .GreaterThanOrEqualTo(0).When(x => x.quantity.HasValue).WithMessage("Quantity must be 0 or greater.");
        }

        public void ApplyCustomValidationRules()
        {
            // Implement any custom validation rules if needed
        }
    }
}
