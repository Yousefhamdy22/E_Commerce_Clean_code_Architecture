using E_Commerce.Application.Base;
using E_Commerce.Domain.Dtos;
using MediatR;

namespace E_Commerce.Application.Features.Authorization.Query.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResults>>
    {
        public int Id { get; set; }
        public ManageUserClaimsQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
