﻿using E_Commerce.Domain.Dtos;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Helpers;
using E_Commerce.Domain.Request;
using E_Commerce.Infastructure.Data;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Services.Implemantation.AuthorizationService
{
    public class AuthorizationServices : IAuthorizationServices
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ECommerceContext _dbContext;
        #endregion
        #region Constructor
        public AuthorizationServices(RoleManager<Role> roleManager, UserManager<User> userManager, ECommerceContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;

        }
        #endregion
        #region Functions

        public async Task<string> AddRoleAsync(string roleName)
        {
            var IdentityRole = new Role();
            IdentityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(IdentityRole);
            if (result.Succeeded)
                return "Success";
            else
                return "Failed";
        }


        public async Task<bool> IsRoleExstists(string roleName)
            => await _roleManager.RoleExistsAsync(roleName);

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }


        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";
            //Chech if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception 
            if (users != null && users.Count() > 0) return "Used";
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            //problem
            var errors = string.Join("-", result.Errors);

            return errors;
        }

        public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }
                rolesList.Add(userrole);
            }
            response.UserRoles = rolesList;
            return response;
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //get user Old Roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //Delete OldRoles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                var selectedRoles = request.UserRoles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesresult.Succeeded)
                    return "FailedToAddNewRoles";
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }

        public async Task<ManageUserClaimsResults> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResults();
            var usercliamsList = new List<UserClaims>();
            response.UserId = user.Id;
           // Get USer Claims
            var userClaims = await _userManager.GetClaimsAsync(user); //edit

          //  create edit get print
            foreach (var claim in ClaimsStore.claims)
            {
                var userclaim = new UserClaims();
                userclaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userclaim.Value = true;
                }
                else
                {
                    userclaim.Value = false;
                }
                usercliamsList.Add(userclaim);
            }
            response.Claims = usercliamsList;
            return response;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequests request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //remove old Claims
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimsResult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var claims = request.Claims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

                var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
                if (!addUserClaimResult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
        #endregion



    }
}
