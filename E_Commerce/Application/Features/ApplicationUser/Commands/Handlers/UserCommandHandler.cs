using E_Commerce.Application.Base;
using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using MediatR;
using System.Collections.Generic;
using AutoMapper;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Services.Abstraction.IApplicationUserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.Application.Features.ApplicationUser.Commands.Handlers
{


    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<UpdateUserCommand, Response<string>>
        , IRequestHandler<DeleteUserCommand, Response<string>>
        , IRequestHandler<ChangePasswordCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IApplicationUserServices _applicationUserServices;
        private readonly ILogger<UserCommandHandler> _logger;


       
        public UserCommandHandler(IMapper mapper , UserManager<User> userManager , 
                                                   IApplicationUserServices applicationUserServices
                                                    ,ILogger<UserCommandHandler> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _applicationUserServices = applicationUserServices;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            
                _logger.LogInformation("Handling AddUserCommand");
            if (request == null)
            {
                _logger.LogError("AddUserCommand request is null");
                return new Response<string>("Invalid request", false);
            }

            _logger.LogInformation($"Processing user: {request.UserName}, Email: {request.Email}");

            var identityUser = _mapper.Map<User>(request);
            var createResult = await _applicationUserServices.AddUserAsync(identityUser, request.PassWord);

            return createResult switch
            {
                "EmailIsExist" => BadRequest<string>("The email address already exists."),
                "EmailInvalid" => BadRequest<string>("The email format is invalid."),
                "UserNameIsExist" => BadRequest<string>("The username already exists."),
                "ErrorInCreateUser" => BadRequest<string>("Failed to create user. Please try again."),
                "ErrorOccurred" => BadRequest<string>("An unexpected error occurred. Please try again."),
                "Success" => Success<string>("User created successfully."),
                _ => BadRequest<string>("An unknown error occurred.")
            };
        }


        //public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("Handling AddUserCommand");
        //    _logger.LogInformation($"UserName: {request.UserName}, Email: {request.Email}");

        //    if (request == null)
        //    {
        //        return new Response<string>("Invalid request", false);
        //    }

        //    var identityUser = _mapper.Map<User>(request);
        //    var createResult = await _applicationUserServices.AddUserAsync(identityUser, request.PassWord);

        //    switch (createResult)
        //    {
        //        case "EmailIsExist":
        //            return BadRequest<string>("The email address already exists.");
        //        case "EmailInvalid":
        //            return BadRequest<string>("The email EmailINvalid.");
        //        case "UserNameIsExist":
        //            return BadRequest<string>("The username already exists.");
        //        case "ErrorInCreateUser":
        //            return BadRequest<string>("Failed to create user. Please try again.");
        //        case "Failed":
        //            return BadRequest<string>("Registration failed. Please try to register again.");
        //        case "Success":
        //            return Success<string>("");
        //        default:

        //            //Console.WriteLine($"Unknown createResult: {createResult}");
        //            return BadRequest<string>("An unknown error occurred.");
        //    }
        //}


        public async Task<Response<string>> Handle(UpdateUserCommand request , CancellationToken cancellationToken)
        {
            //check if user is exist
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (oldUser == null)
                return NotFound<string>();
            //mapping
            var newUser = _mapper.Map(request, oldUser);
            //if username is Exist
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            //username is Exist
            if (userByUserName != null) return BadRequest<string>("UserNameIsAlreadyExsists");
            //update
            var result = await _userManager.UpdateAsync(newUser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            //message
            return Success("Update");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByIdAsync(request.Id.ToString());
            if (User == null)
                return NotFound<string>("NotFound");
            var Result = await _userManager.DeleteAsync(User);
            if (!Result.Succeeded)
                return BadRequest<string>("FailedToDeleteUser");
            return Success<string>("Deleted");
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null)
                return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success<string>("Success");
        }



    }


}
