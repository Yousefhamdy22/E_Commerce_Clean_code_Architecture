using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.EncryptColumn.Attribute;
using System.ComponentModel.DataAnnotations;


namespace E_Commerce.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public string Role { get; set; }

        [EncryptColumn]
        public string? Code { get; set; }


        public ICollection<Order>? Orders { get; set; }
        public ICollection<PaymentDetails>? PaymentDetails { get; set; }
        public ICollection<Product>? Products { get; set; }
   
        public ICollection<ShoppingCart>? shoppingCarts { get; set; }
        public ICollection<Address>? Addresses { get; set; }

        [InverseProperty("user")]
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
