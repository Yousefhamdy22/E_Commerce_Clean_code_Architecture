using E_Commerce.Application.Features.ApplicationUser.Commands.Handlers;
using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Data;


namespace E_Commerce.Application.Features.ApplicationUser.Commands.Validation
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {

        public AddUserValidator()  //IStringLocalizer<SharedResourses> localizer)
        {
            //  _localizer = localizer;
            ApplyValidationsRules();
            //  ApplyCustomValidationsRules();
        }


     
        public void ApplyValidationsRules()
        {
           

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User Name is required.")
                .MaximumLength(100).WithMessage("User Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid Email is required.");

            RuleFor(x => x.PassWord)
                .NotEmpty().WithMessage("Password is required.");

          

            

        }

        public void ApplyCustomValidationsRules()
        {

        }
    }
}

