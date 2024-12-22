using E_Commerce.Application.Base;
using MediatR;

namespace E_Commerce.Application.Features.ApplicationUser.Commands.Models
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
      //  public string? Address { get; set; }
     //   public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
      //  public string? ProfileImage { get; set; }
    }
}
