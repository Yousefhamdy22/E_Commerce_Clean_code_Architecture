
using E_Commerce.Application.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Features.ApplicationUser.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
       
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }

       public string? PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? PassWord { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }



     
    }
}
