using E_Commerce.Domain.Dtos;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Request;

namespace E_Commerce.Services.Abstraction.IAuthorizationServices
{
    public interface IAuthorizationServices
    {
        Task<string> AddRoleAsync(string roleName);
        Task<bool> IsRoleExstists(string roleName);
        Task<List<Role>> GetRolesAsync();

        //  Task<string> EditRoleAsync(EditRoleCommand editRoleRequest);
        Task<string> DeleteRoleAsync(int roleId);
        Task<Role> GetRoleById(int id);
        Task<ManageUserRolesResult> ManageUserRolesData(User user);
        Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        Task<string> UpdateUserClaims(UpdateUserClaimsRequests request);

        Task<ManageUserClaimsResults> ManageUserClaimData(User user);
        //Task<string> UpdateUserClaims(UpdateUserClaimsRequests request);
    }
}
