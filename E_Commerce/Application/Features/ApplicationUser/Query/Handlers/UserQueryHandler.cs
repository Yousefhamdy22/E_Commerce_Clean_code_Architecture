using AutoMapper;
using E_Commerce.Application.Base;
using E_Commerce.Application.Features.ApplicationUser.Query.Models;
using E_Commerce.Application.Features.ApplicationUser.Query.Result;
using E_Commerce.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Features.ApplicationUser.Query.Handlers
{
    public class UserQueryHandler : ResponseHandler,
         IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserQueryHandler(IMapper mapper,
                                  UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion
        #region Handle Functions       
        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetUserByIdResponse>("NotFound");
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
        #endregion
    }
}
