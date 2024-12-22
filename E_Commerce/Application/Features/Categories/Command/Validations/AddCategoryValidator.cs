using E_Commerce.Application.Features.Categories.Command.Models;
using FluentValidation;

namespace E_Commerce.Application.Features.Categories.Command.Validations
{
    public class AddCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        #region Fields
        //   private readonly IStringLocalizer<SharedResourses> _localizer;

        #endregion
        #region Constructors
        public AddCategoryValidator()//IStringLocalizer<SharedResourses> localizer)
        {
            //  _localizer = localizer;
            ApplyValidationsRules();
            //  ApplyCustomValidationsRules();
        }

        #endregion
        #region Handle Functions
        public void ApplyValidationsRules()
        {

            // New rules for additional properties
            RuleFor(x => x.IsDeleted)
                .NotNull().WithMessage("IsDeleted cannot be null.");

            RuleFor(x => x.Name)
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");


            RuleFor(x => x.TotalProducts)
                .GreaterThanOrEqualTo(0).WithMessage("Total Products must be 0 or greater.");

        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
