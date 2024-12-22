namespace E_Commerce.Domain.Dtos
{
    public class ManageUserClaimsResults
    {
        public int UserId { get; set; }
        public List<UserClaims> Claims { get; set; }

    }
    // top level class cannot be any thing other internal or public
    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
