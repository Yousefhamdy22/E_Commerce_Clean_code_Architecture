using E_Commerce.Application.Features.ApplicationUser.Query.Result;
using MediatR;
using E_Commerce.Application.Base;

namespace E_Commerce.Application.Features.ApplicationUser.Query.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int Id)
        {
            this.Id = Id;
        }

    }
}
