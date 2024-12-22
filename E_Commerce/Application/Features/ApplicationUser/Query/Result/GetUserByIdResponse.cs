
using System;
using System.Collections.Generic;

namespace E_Commerce.Application.Features.ApplicationUser.Query.Result
{
    public class GetUserByIdResponse
    {
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? ProfileImage { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
