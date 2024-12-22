using System.Security.Claims;

namespace E_Commerce.Domain.Helpers
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create","False"),
            new Claim("Edit","False"),
            new Claim("Delete","False"),
        };
    }
}
